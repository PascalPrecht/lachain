﻿using System;
using Google.Protobuf;
using Phorkus.Core.Config;
using Phorkus.Core.DI;
using Phorkus.Core.DI.Modules;
using Phorkus.Core.DI.SimpleInjector;
using Phorkus.Core.VM;
using Phorkus.Proto;
using Phorkus.Storage.State;
using Phorkus.Utility;
using Phorkus.Utility.Utils;
using Phorkus.WebAssembly;

namespace Phorkus.Benchmark
{
    public class VirtualMachineBenchmark : IBootstrapper
    {
        private readonly IContainer _container;
        
        public VirtualMachineBenchmark()
        {
            var containerBuilder = new SimpleInjectorContainerBuilder(
                new ConfigManager("config.json"));

            containerBuilder.RegisterModule<LoggingModule>();
            containerBuilder.RegisterModule<BlockchainModule>();
            containerBuilder.RegisterModule<ConfigModule>();
            containerBuilder.RegisterModule<CryptographyModule>();
            containerBuilder.RegisterModule<MessagingModule>();
            containerBuilder.RegisterModule<NetworkModule>();
            containerBuilder.RegisterModule<StorageModule>();

            _container = containerBuilder.Build();
        }
        
        public void Start(string[] args)
        {
            var virtualMachine = _container.Resolve<IVirtualMachine>();
            var stateManager = _container.Resolve<IStateManager>();
            
            var hash = UInt160Utils.Zero;
            var contract = new Contract
            {
                Hash = hash,
                Version = ContractVersion.Wasm,
                Wasm = ByteString.CopyFrom("0061736d010000000131096000006000017f60037f7f7f0060027f7f0060017f0060017f017f60027f7e017f60027f7f017f60057f7e7e7e7e017f0283010703656e760d6765745f63616c6c5f73697a65000103656e760f636f70795f63616c6c5f76616c7565000203656e760977726974655f6c6f67000303656e760a7365745f72657475726e000303656e760c6c6f61645f73746f72616765000203656e760a6765745f73656e646572000403656e760c736176655f73746f726167650002031c1b0005000005000400000000000304050305060707020203080707070405017001050505030100020615037f01418088040b7f00418088040b7f004180080b072d04066d656d6f727902000b5f5f686561705f6261736503010a5f5f646174615f656e6403020573746172740009090a010041010b040c0e10120af1121b02000b290020004200370300200041186a4200370300200041106a4200370300200041086a420037030020000bbf0201027f23808080800041106b22002480808080000240024010808080800041034b0d00108a808080000c010b410041042000410c6a108b8080800022011081808080002001410410828080800002400240024002400240200028020c220141dc9bd8c0014a0d00200141bbb996c87a460d01200141beda8beb7d460d02200141b3cffaca00470d05418180808000108d80808000200041106a2480808080000f0b200141b184828507460d02200141dde5e19d02460d03200141dd9bd8c001470d04418280808000108d80808000200041106a2480808080000f0b108f80808000200041106a2480808080000f0b418380808000108d80808000200041106a2480808080000f0b109180808000200041106a2480808080000f0b418480808000108d80808000200041106a2480808080000f0b200041106a2480808080000b3e01017f23808080800041106b2200248080808000200041effdb6f57d36020c2000410c6a108b808080004104108280808000200041106a2480808080000b040020000b02000b4501017f23808080800041106b22012480808080001080808080001a410441042001410f6a108b80808000108180808000200011808080800000200141106a2480808080000b3e01017f23808080800041106b2200248080808000200041baf9aef50136020c2000410c6a108b808080004104108380808000200041106a2480808080000b6e01017f23808080800041c0006b22002480808080001080808080001a4104411c200041206a108b808080001081808080002000200041206a36021c200041086a2000411c6a109380808000200041086a2000411c6a109580808000109680808000200041c0006a2480808080000b02000b6301017f23808080800041c0006b22002480808080001080808080001a41044118200041206a108b808080001081808080002000200041206a36021c200041086a2000411c6a109380808000200041086a109480808000200041c0006a2480808080000b02000b1e0020001097808080002100200120012802002000109f808080003602000b4d01017f23808080800041206b220124808080800020011088808080001a2000108b8080800041142001108b80808000220010848080800020004120108380808000200141206a2480808080000b3e01017f23808080800041106b2201248080808000200020002802002001410c6a10a180808000360200200128020c2100200141106a24808080800020000bf80102047f017e2380808080004180016b2202248080808000200241e8006a1097808080001a200241e8006a108b808080002203108580808000200241c8006a108880808000210420034114200241c8006a108b8080800022051084808080002004200241286a2001ad22061098808080001099808080001a200341142005108680808000200241286a10888080800021032000108b8080800022004114200241286a108b8080800022011084808080002003200241086a2006109880808000109a808080001a200041142001108680808000200241013a0008200241086a108b80808000410110838080800020024180016a2480808080000b1f0020004200370000200041106a4100360000200041086a420037000020000b26002000420037030820002001370300200041186a4200370300200041106a420037030020000b6801017f23808080800041206b2202248080808000200220002001109b80808000200041186a200241186a290300370300200041106a200241106a290300370300200041086a200241086a29030037030020002002290300370300200241206a24808080800020000b6801017f23808080800041206b2202248080808000200220002001109c80808000200041186a200241186a290300370300200041106a200241106a290300370300200041086a200241086a29030037030020002002290300370300200241206a24808080800020000b5a01017f23808080800041e0006b2203248080808000200341206a2002109d80808000200341c0006a2001200341206a109c808080002000200341c0006a20034201109880808000109c80808000200341e0006a2480808080000b840201047e20004200420042004200109e8080800021002002290300220320012903007c2204200029030022057c21060240024020042003540d00200542005220065071450d010b2000200029030842017c3703080b200020063703002002290308220320012903087c2204200029030822057c21060240024020042003540d00200542005220065071450d010b2000200029031042017c3703100b200041086a20063703002002290310220320012903107c2204200029031022057c21060240024020042003540d00200542005220065071450d010b2000200029031842017c3703180b200041106a20063703002000200229031820012903187c20002903187c3703180b3e00200010888080800022002001290300427f8537030020002001290308427f8537030820002001290310427f8537031020002001290318427f853703180b20002000200437031820002003370310200020023703082000200137030020000bc10301017f20002d000021022001410010a08080800020023a000020002d000121022001410110a08080800020023a000020002d000221022001410210a08080800020023a000020002d000321022001410310a08080800020023a000020002d000421022001410410a08080800020023a000020002d000521022001410510a08080800020023a000020002d000621022001410610a08080800020023a000020002d000721022001410710a08080800020023a000020002d000821022001410810a08080800020023a000020002d000921022001410910a08080800020023a000020002d000a21022001410a10a08080800020023a000020002d000b21022001410b10a08080800020023a000020002d000c21022001410c10a08080800020023a000020002d000d21022001410d10a08080800020023a000020002d000e21022001410e10a08080800020023a000020002d000f21022001410f10a08080800020023a000020002d001021022001411010a08080800020023a000020002d001121022001411110a08080800020023a000020002d001221022001411210a08080800020023a000020002d001321022001411310a08080800020023a0000200041146a0b0700200020016a0b110020012000280000360200200041046a0b008e0b046e616d6501860b22000d6765745f63616c6c5f73697a65010f636f70795f63616c6c5f76616c7565020977726974655f6c6f67030a7365745f72657475726e040c6c6f61645f73746f72616765050a6765745f73656e646572060c736176655f73746f7261676507115f5f7761736d5f63616c6c5f63746f7273081b6c61636861696e3a3a75696e743235363a3a75696e743235362829090573746172740a116d6574686f645f66616c6c6261636b28290b206c61636861696e3a3a6765745f6f666673657428766f696420636f6e73742a290c0f65726332305f617070726f766528290d32766f6964206c61636861696e3a3a696e766f6b655f636f6e74726163745f6d6574686f643c3e28766f696420282a292829290e1465726332305f746f74616c5f737570706c7928290f6e766f6964206c61636861696e3a3a696e766f6b655f636f6e74726163745f6d6574686f643c6c61636861696e3a3a616464726573732c20756e7369676e656420696e743e28766f696420282a29286c61636861696e3a3a616464726573732c20756e7369676e656420696e742929101165726332305f616c6c6f77616e636528291152766f6964206c61636861696e3a3a696e766f6b655f636f6e74726163745f6d6574686f643c6c61636861696e3a3a616464726573733e28766f696420282a29286c61636861696e3a3a616464726573732929121565726332305f7472616e736665725f66726f6d282913466c61636861696e3a3a5f617267756d656e745f6465636f6465725f3c6c61636861696e3a3a616464726573733e3a3a6465636f646528756e7369676e656420636861722a2629142265726332305f62616c616e63655f6f66286c61636861696e3a3a616464726573732915426c61636861696e3a3a5f617267756d656e745f6465636f6465725f3c756e7369676e656420696e743e3a3a6465636f646528756e7369676e656420636861722a2629162e65726332305f7472616e73666572286c61636861696e3a3a616464726573732c20756e7369676e656420696e7429171b6c61636861696e3a3a616464726573733a3a616464726573732829182d6c61636861696e3a3a75696e743235363a3a75696e7432353628756e7369676e6564206c6f6e67206c6f6e672919356c61636861696e3a3a75696e743235363a3a6f70657261746f722d3d286c61636861696e3a3a75696e7432353620636f6e737426291a356c61636861696e3a3a75696e743235363a3a6f70657261746f722b3d286c61636861696e3a3a75696e7432353620636f6e737426291b3a6c61636861696e3a3a75696e743235363a3a6f70657261746f722d286c61636861696e3a3a75696e7432353620636f6e7374262920636f6e73741c3a6c61636861696e3a3a75696e743235363a3a6f70657261746f722b286c61636861696e3a3a75696e7432353620636f6e7374262920636f6e73741d236c61636861696e3a3a75696e743235363a3a6f70657261746f727e282920636f6e73741e696c61636861696e3a3a75696e743235363a3a75696e7432353628756e7369676e6564206c6f6e67206c6f6e672c20756e7369676e6564206c6f6e67206c6f6e672c20756e7369676e6564206c6f6e67206c6f6e672c20756e7369676e6564206c6f6e67206c6f6e67291f586c61636861696e3a3a5f617267756d656e745f6465636f6465725f3c6c61636861696e3a3a616464726573733e3a3a6465636f646528756e7369676e656420636861722a2c206c61636861696e3a3a61646472657373262920216c61636861696e3a3a616464726573733a3a6f70657261746f725b5d28696e742921506c61636861696e3a3a5f617267756d656e745f6465636f6465725f3c756e7369676e656420696e743e3a3a6465636f646528756e7369676e656420636861722a2c20756e7369676e656420696e74262900250970726f647563657273010c70726f6365737365642d62790105636c616e6705392e302e30".HexToBytes())
            };
            if (!virtualMachine.VerifyContract(contract.Wasm.ToByteArray()))
                throw new RuntimeException("Unable to validate smart-contract code");
            
            var snapshot = stateManager.NewSnapshot();
            snapshot.Contracts.AddContract(UInt160Utils.Zero, contract);
            stateManager.Approve();
            
            Console.WriteLine("Contract Hash: " + hash.Buffer.ToHex());
            
            var currentTime = TimeUtils.CurrentTimeMillis();
            stateManager.NewSnapshot();
            /*var status = virtualMachine.InvokeContract(contract, UInt160Utils.Zero, new byte[] { }); 
            if (status != ExecutionStatus.Ok)
            {
                stateManager.Rollback();
                Console.WriteLine("Contract execution failed: " + status);
                goto exit_mark;
            }*/

            var sender = "0x6bc32575acb8754886dc283c2c8ac54b1bd93195".HexToBytes().ToUInt160();
            var to = "0xfd893ce89186fc6861d339cb6ab5d75458e3daf3".HexToBytes().ToUInt160();
            
            /* give to sender 1 token */
            stateManager.CurrentSnapshot.Storage.SetValue(contract.Hash, sender.ToUInt256(), Money.FromDecimal(1).ToUInt256());
            
            /* ERC-20: totalSupply (0x18160ddd) */
            var input = new byte[24];
            input[3] = 0x18;
            input[2] = 0x16;
            input[1] = 0x0d;
            input[0] = 0xdd;
            for (var i = 0; i < 20; i++)
                input[i + 4] = sender.Buffer[i];
            var status = virtualMachine.InvokeContract(contract, sender, input); 
            if (status != ExecutionStatus.Ok)
            {
                stateManager.Rollback();
                Console.WriteLine("Contract execution failed: " + status);
                goto exit_mark;
            }
            
            /* ERC-20: balanceOf (0x70a08231) */
            input = new byte[24];
            input[3] = 0x70;
            input[2] = 0xa0;
            input[1] = 0x82;
            input[0] = 0x31;
            for (var i = 0; i < 20; i++)
                input[i + 4] = sender.Buffer[i];
            status = virtualMachine.InvokeContract(contract, sender, input); 
            if (status != ExecutionStatus.Ok)
            {
                stateManager.Rollback();
                Console.WriteLine("Contract execution failed: " + status);
                goto exit_mark;
            }
            
            /* ERC-20: transfer (0xa9059cbb) */
            input = new byte[4 + 20 + 4];
            input[3] = 0xa9;
            input[2] = 0x05;
            input[1] = 0x9c;
            input[0] = 0xbb;
            for (var i = 0; i < 20; i++)
                input[i + 4] = to.Buffer[i];
            input[24] = 0x00;
            input[25] = 0x00;
            input[26] = 0x00;
            input[27] = 0x01;
            status = virtualMachine.InvokeContract(contract, sender, input); 
            if (status != ExecutionStatus.Ok)
            {
                stateManager.Rollback();
                Console.WriteLine("Contract execution failed: " + status);
                goto exit_mark;
            }
            
            /* ERC-20: balanceOf (0x70a08231) */
            input = new byte[24];
            input[3] = 0x70;
            input[2] = 0xa0;
            input[1] = 0x82;
            input[0] = 0x31;
            status = virtualMachine.InvokeContract(contract, sender, input);
            if (status != ExecutionStatus.Ok)
            {
                stateManager.Rollback();
                Console.WriteLine("Contract execution failed: " + status);
                goto exit_mark;
            }
            
//            input = new byte[24 + 32];
//            input[0] = 2;
//            input[24] = 10;
//            status = virtualMachine.InvokeContract(contract, UInt160Utils.Zero, input); 
//            if (status != ExecutionStatus.Ok)
//            {
//                stateManager.Rollback();
//                Console.WriteLine("Contract execution failed: " + status);
//                goto exit_mark;
//            }
//            
//            input = new byte[24];
//            input[0] = 0xff;
//            status = virtualMachine.InvokeContract(contract, UInt160Utils.Zero, input); 
//            if (status != ExecutionStatus.Ok)
//            {
//                stateManager.Rollback();
//                Console.WriteLine("Contract execution failed: " + status);
//                goto exit_mark;
//            }
            
            stateManager.Approve();
            exit_mark:
            var elapsedTime = TimeUtils.CurrentTimeMillis() - currentTime;
            Console.WriteLine("Elapsed Time: " + elapsedTime + "ms");
        }
    }
}