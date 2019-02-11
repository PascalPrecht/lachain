﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Phorkus.Crypto;
using Phorkus.Proto;
using Phorkus.Utility.Utils;
using Phorkus.WebAssembly;
using Phorkus.WebAssembly.Runtime;

// ReSharper disable MemberCanBePrivate.Global

namespace Phorkus.Core.VM
{
    public class ExternalHandler : IExternalHandler
    {
        private const string EnvModule = "env";

        private static ExecutionStatus DoInternalCall(UInt160 caller, UInt160 address, byte[] input,
            out ExecutionFrame frame)
        {
            var contract = VirtualMachine.BlockchainSnapshot.Contracts.GetContractByHash(address);
            if (contract is null)
            {
                frame = null;
                return ExecutionStatus.NoSuchContract;
            }

            var status = ExecutionFrame.FromInternalCall(
                contract.Wasm.ToByteArray(), caller, address, input,
                VirtualMachine.BlockchainInterface, out frame
            );
            if (status != ExecutionStatus.Ok) return status;
            VirtualMachine.ExecutionFrames.Push(frame);
            return status;
        }

        private static byte[] SafeCopyFromMemory(UnmanagedMemory memory, int offset, int length)
        {
            if (length < 0 || offset < 0)
                return null;
            if (offset + length > memory.Size)
                return null;
            var buffer = new byte[length];
            try
            {
                Marshal.Copy(IntPtr.Add(memory.Start, offset), buffer, 0, length);
            }
            catch (ArgumentNullException)
            {
                return null;
            }

            return buffer;
        }

        private static bool SafeCopyToMemory(UnmanagedMemory memory, byte[] data, int offset)
        {
            if (offset < 0 || offset + data.Length > memory.Size)
                return false;
            try
            {
                Marshal.Copy(data, 0, IntPtr.Add(memory.Start, offset), data.Length);
            }
            catch (Exception e) when (e is ArgumentNullException || e is ArgumentOutOfRangeException)
            {
                return false;
            }

            return true;
        }
        
        public static int Handler_Env_GetCallValue(int offset)
        {
            var frame = VirtualMachine.ExecutionFrames.Peek();
            if (offset < 0 || offset >= frame.Input.Length)
            {
                throw new RuntimeException("Bad getcallvalue call");
            }

            return frame.Input[offset];
        }

        public static int Handler_Env_GetCallSize()
        {
            var frame = VirtualMachine.ExecutionFrames.Peek();
            return frame.Input.Length;
        }

        public static void Handler_Env_CopyCallValue(int from, int to, int offset)
        {
            var frame = VirtualMachine.ExecutionFrames.Peek();
            if (from < 0 || to > frame.Input.Length || from > to)
                throw new RuntimeException("Copy to contract memory failed: bad range");
            if (!SafeCopyToMemory(frame.Memory, frame.Input.Skip(from).Take(to - from).ToArray(), offset))
                throw new RuntimeException("Copy to contract memory failed");
        }

        public static void Handler_Env_WriteLog(int offset, int length)
        {
            // TODO: check signs
            var frame = VirtualMachine.ExecutionFrames.Peek();
            var buffer = SafeCopyFromMemory(frame.Memory, offset, length);
            if (buffer == null)
            {
                throw new RuntimeException("Bad call to writelog");
            }
            Console.WriteLine($"Contract ({frame.CurrentAddress}) logged: {buffer.ToHex()}");
        }

        public static int Handler_Env_InvokeContract(
            int callSignatureOffset, int inputLength, int inputOffset, int valueOffset, int returnValueOffset)
        {
            var frame = VirtualMachine.ExecutionFrames.Peek();
            var signatureBuffer = SafeCopyFromMemory(frame.Memory, callSignatureOffset, 20);
            var inputBuffer = SafeCopyFromMemory(frame.Memory, inputOffset, inputLength);
            if (signatureBuffer is null || inputBuffer is null)
            {
                throw new RuntimeException("Bad call to call function");
            }
            var address = signatureBuffer.Take(20).ToArray().ToUInt160();
            var status = DoInternalCall(frame.CurrentAddress, address, inputBuffer, out var newFrame);
            if (status != ExecutionStatus.Ok)
            {
                throw new RuntimeException("Cannot invoke call: " + status);
            }

            status = newFrame.Execute(); 
            if (status != ExecutionStatus.Ok)
            {
                throw new RuntimeException("Cannot invoke call: " + status);
            }
            newFrame = VirtualMachine.ExecutionFrames.Pop();
            var returned = newFrame.ReturnValue;
            if (!SafeCopyToMemory(frame.Memory, returned, returnValueOffset))
            {
                throw new RuntimeException("Cannot invoke call: cannot pass return value");
            }
            return 0;
        }
        
        public static void Handler_Env_LoadStorage(int keyOffset, int keyLength, int valueOffset)
        {
            var frame = VirtualMachine.ExecutionFrames.Peek();
            if (keyLength > 32)
                throw new Exception("Key length can't be greater than 32 bytes");
            var key = SafeCopyFromMemory(frame.Memory, keyOffset, keyLength);
            if (key is null)
                throw new RuntimeException("Bad call to LOADSTORAGE");
            if (key.Length < 32)
                key = _AlignTo32(key);
            var value = VirtualMachine.BlockchainSnapshot.Storage.GetValue(frame.CurrentAddress, key.ToUInt256());
            if (!SafeCopyToMemory(frame.Memory, value.Buffer.ToByteArray(), valueOffset))
                throw new RuntimeException("Cannot copy storageload result to memory");
        }
        
        public static void Handler_Env_SaveStorage(int keyOffset, int keyLength, int valueOffset)
        {
            var frame = VirtualMachine.ExecutionFrames.Peek();
            if (keyLength > 32)
                throw new Exception("Key length can't be greater than 32 bytes");
            var key = SafeCopyFromMemory(frame.Memory, keyOffset, keyLength);
            if (key is null)
                throw new RuntimeException("Bad call to SAVESTORAGE");
            if (key.Length < 32)
                key = _AlignTo32(key);
            var value = SafeCopyFromMemory(frame.Memory, valueOffset, 32);
            if (value is null)
                throw new RuntimeException("Bad call to SAVESTORAGE");
            VirtualMachine.BlockchainSnapshot.Storage.SetValue(frame.CurrentAddress, key.ToUInt256(), value.ToUInt256());
        }

        private static byte[] _AlignTo32(byte[] buffer)
        {
            if (buffer.Length == 32)
                return buffer;
            var result = new byte[32];
            Array.Copy(buffer, 0, result, 0, buffer.Length);
            return result;
        }
        
        public static void Handler_Env_SetReturn(int offset, int length)
        {
            var frame = VirtualMachine.ExecutionFrames.Peek();
            var ret = SafeCopyFromMemory(frame.Memory, offset, length);
            if (ret is null)
                throw new RuntimeException("Bad call to SETRETURN");
            frame.ReturnValue = ret;
        }
        
        public static void Handler_Env_GetSender(int dataOffset)
        {
            var frame = VirtualMachine.ExecutionFrames.Peek();
            var data = frame.Sender.Buffer.ToByteArray();
            var ret = SafeCopyToMemory(frame.Memory, data, dataOffset);
            if (!ret)
                throw new RuntimeException("Bad call to GETSENDER");
        }
        
        public static void Handler_Env_Halt(int haltCode)
        {
            throw new HaltException(haltCode);
        }

        public static void Handler_Env_Keccak256(int dataOffset, int dataLength, int resultOffset)
        {
            var frame = VirtualMachine.ExecutionFrames.Peek();
            var data = SafeCopyFromMemory(frame.Memory, dataOffset, dataLength);
            var result = data.Keccak256();
            SafeCopyToMemory(frame.Memory, result, resultOffset);
        }
        
        public static void Handler_Env_Sha256(int dataOffset, int dataLength, int resultOffset)
        {
            var frame = VirtualMachine.ExecutionFrames.Peek();
            var data = SafeCopyFromMemory(frame.Memory, dataOffset, dataLength);
            var result = data.Sha256();
            SafeCopyToMemory(frame.Memory, result, resultOffset);
        }
        
        public static void Handler_Env_Ripemd160(int dataOffset, int dataLength, int resultOffset)
        {
            var frame = VirtualMachine.ExecutionFrames.Peek();
            var data = SafeCopyFromMemory(frame.Memory, dataOffset, dataLength);
            var result = data.Ripemd160();
            SafeCopyToMemory(frame.Memory, result, resultOffset);
        }
        
        public static void Handler_Env_Murmur3(int dataOffset, int dataLength, int resultOffset, int seed)
        {
            var frame = VirtualMachine.ExecutionFrames.Peek();
            var data = SafeCopyFromMemory(frame.Memory, dataOffset, dataLength);
            var result = data.Murmur3((uint) seed);
            SafeCopyToMemory(frame.Memory, result, resultOffset);
        }
        
        public static void Handler_Env_Recover(int messageOffset, int messageLength, int signatureOffset, int resultOffset)
        {
            var frame = VirtualMachine.ExecutionFrames.Peek();
            var message = SafeCopyFromMemory(frame.Memory, messageOffset, messageLength);
            var sig = SafeCopyFromMemory(frame.Memory, signatureOffset, SignatureUtils.Length);
            var publicKey = VirtualMachine.Crypto.RecoverSignature(message, sig);
            SafeCopyToMemory(frame.Memory, publicKey, resultOffset);
        }
        
        public static void Handler_Env_Verify(int messageOffset, int messageLength, int signatureOffset, int publicKeyOffset, int resultOffset)
        {
            var frame = VirtualMachine.ExecutionFrames.Peek();
            var message = SafeCopyFromMemory(frame.Memory, messageOffset, messageLength);
            var sig = SafeCopyFromMemory(frame.Memory, signatureOffset, SignatureUtils.Length);
            var publicKey = SafeCopyFromMemory(frame.Memory, publicKeyOffset, CryptoUtils.PublicKeyLength);
            var result = VirtualMachine.Crypto.VerifySignature(message, sig, publicKey);
            SafeCopyToMemory(frame.Memory, new[]{ result ? (byte) 1 : (byte) 0 }, resultOffset);
        }
        
        public IEnumerable<FunctionImport> GetFunctionImports()
        {
            return new[]
            {
                /* basic system methods */
                new FunctionImport(EnvModule, "get_call_value",
                    typeof(ExternalHandler).GetMethod(nameof(Handler_Env_GetCallValue))),
                new FunctionImport(EnvModule, "get_call_size",
                    typeof(ExternalHandler).GetMethod(nameof(Handler_Env_GetCallSize))),
                new FunctionImport(EnvModule, "copy_call_value",
                    typeof(ExternalHandler).GetMethod(nameof(Handler_Env_CopyCallValue))),
                new FunctionImport(EnvModule, "invoke_contract",
                    typeof(ExternalHandler).GetMethod(nameof(Handler_Env_InvokeContract))),
                new FunctionImport(EnvModule, "write_log",
                    typeof(ExternalHandler).GetMethod(nameof(Handler_Env_WriteLog))),
                new FunctionImport(EnvModule, "load_storage",
                    typeof(ExternalHandler).GetMethod(nameof(Handler_Env_LoadStorage))),
                new FunctionImport(EnvModule, "save_storage",
                    typeof(ExternalHandler).GetMethod(nameof(Handler_Env_SaveStorage))),
                new FunctionImport(EnvModule, "set_return",
                    typeof(ExternalHandler).GetMethod(nameof(Handler_Env_SetReturn))),
                new FunctionImport(EnvModule, "get_sender",
                    typeof(ExternalHandler).GetMethod(nameof(Handler_Env_GetSender))),
                new FunctionImport(EnvModule, "halt",
                    typeof(ExternalHandler).GetMethod(nameof(Handler_Env_Halt))),
                /* crypto hash bindings */
                new FunctionImport(EnvModule, "crypto_keccak256",
                    typeof(ExternalHandler).GetMethod(nameof(Handler_Env_Keccak256))),
                new FunctionImport(EnvModule, "crypto_sha256",
                    typeof(ExternalHandler).GetMethod(nameof(Handler_Env_Sha256))),
                new FunctionImport(EnvModule, "crypto_ripemd160",
                    typeof(ExternalHandler).GetMethod(nameof(Handler_Env_Ripemd160))),
                new FunctionImport(EnvModule, "crypto_murmur3",
                    typeof(ExternalHandler).GetMethod(nameof(Handler_Env_Murmur3))),
                /* cryptography methods */
                new FunctionImport(EnvModule, "crypto_recover",
                    typeof(ExternalHandler).GetMethod(nameof(Handler_Env_Recover))),
                new FunctionImport(EnvModule, "crypto_verify",
                    typeof(ExternalHandler).GetMethod(nameof(Handler_Env_Verify))),
            };
        }
    }
}