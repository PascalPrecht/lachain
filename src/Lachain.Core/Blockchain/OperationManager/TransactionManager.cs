﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Lachain.Core.Blockchain.ContractManager;
using Lachain.Core.Blockchain.Interface;
using Lachain.Core.Utils;
using Lachain.Core.VM;
using Lachain.Crypto;
using Lachain.Proto;
using Lachain.Storage.State;
using Lachain.Utility.Utils;

namespace Lachain.Core.Blockchain.OperationManager
{
    public class TransactionManager : ITransactionManager
    {
        private readonly ITransactionVerifier _transactionVerifier;
        private readonly IReadOnlyDictionary<TransactionType, ITransactionExecuter> _transactionPersisters;
        private readonly ICrypto _crypto = CryptoProvider.GetCrypto();
        private readonly IStateManager _stateManager;

        public TransactionManager(
            ITransactionVerifier transactionVerifier,
            IVirtualMachine virtualMachine,
            IContractRegisterer contractRegisterer,
            IStateManager stateManager)
        {
            _transactionPersisters = new Dictionary<TransactionType, ITransactionExecuter>
            {
                {TransactionType.Transfer, new ContractTransactionExecuter(contractRegisterer, virtualMachine)},
                {TransactionType.Deploy, new DeployTransactionExecuter(virtualMachine)}
            };
            _stateManager = stateManager ?? throw new ArgumentNullException(nameof(stateManager));
            _transactionVerifier = transactionVerifier ?? throw new ArgumentNullException(nameof(transactionVerifier));

            transactionVerifier.OnTransactionVerified += (sender, transaction) =>
                _verifiedTransactions.TryAdd(transaction.Hash, transaction.Hash);
        }

        private readonly ConcurrentDictionary<UInt256, UInt256> _verifiedTransactions
            = new ConcurrentDictionary<UInt256, UInt256>();

        public event EventHandler<TransactionReceipt>? OnTransactionPersisted;
        public event EventHandler<TransactionReceipt>? OnTransactionFailed;
        public event EventHandler<TransactionReceipt>? OnTransactionExecuted;
        public event EventHandler<TransactionReceipt>? OnTransactionSigned;

        public TransactionReceipt? GetByHash(UInt256 transactionHash)
        {
            return _stateManager.CurrentSnapshot.Transactions.GetTransactionByHash(transactionHash);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public OperatingError Execute(Block block, TransactionReceipt receipt, IBlockchainSnapshot snapshot)
        {
            var transactionRepository = _stateManager.CurrentSnapshot.Transactions;
            /* check transaction with this hash in database */
            if (transactionRepository.GetTransactionByHash(receipt.Hash) != null)
                return OperatingError.AlreadyExists;
            /* verify transaction */
            var verifyError = Verify(receipt, block.Header.Index == 0);
            if (verifyError != OperatingError.Ok)
                return verifyError;
            /* maybe we don't need this check, but I'm afraid */
            if (!receipt.Transaction.Keccak().Equals(receipt.Hash))
                return OperatingError.HashMismatched;
            /* check is transaction type supported */
            if (!_transactionPersisters.ContainsKey(receipt.Transaction.Type))
                return OperatingError.UnsupportedTransaction;
            var persister = _transactionPersisters[receipt.Transaction.Type];
            if (persister == null)
                return OperatingError.UnsupportedTransaction;
            /* check transaction nonce */
            var nonce = transactionRepository.GetTotalTransactionCount(receipt.Transaction.From);
            if (nonce != receipt.Transaction.Nonce)
                return OperatingError.InvalidNonce;
            /* try to persist transaction */
            var result = persister.Execute(block, receipt, snapshot);
            if (result != OperatingError.Ok)
            {
                OnTransactionFailed?.Invoke(this, receipt);
                return result;
            }

            /* finalize transaction state */
            OnTransactionExecuted?.Invoke(this, receipt);
            return OperatingError.Ok;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public TransactionReceipt Sign(Transaction transaction, ECDSAKeyPair keyPair)
        {
            /* use raw byte arrays to sign transaction hash */
            var message = transaction.KeccakBytes();
            var signature = _crypto.Sign(message, keyPair.PrivateKey.Buffer.ToByteArray());
            /* we're afraid */
            var pubKey = _crypto.RecoverSignature(message, signature);
            if (!pubKey.SequenceEqual(keyPair.PublicKey.Buffer.ToByteArray()))
                throw new InvalidKeyPairException();
            var signed = new TransactionReceipt
            {
                Transaction = transaction,
                Hash = transaction.Keccak(),
                Signature = signature.ToSignature()
            };
            OnTransactionSigned?.Invoke(this, signed);
            return signed;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public OperatingError Verify(TransactionReceipt transaction)
        {
            return Verify(transaction, false);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private OperatingError Verify(TransactionReceipt acceptedTransaction, bool isGenesis)
        {
            if (!Equals(acceptedTransaction.Hash, acceptedTransaction.Transaction.Keccak()))
                return OperatingError.HashMismatched;
            if (isGenesis)
                return !acceptedTransaction.Signature.IsZero() ? OperatingError.InvalidSignature : OperatingError.Ok;

            var result = VerifySignature(acceptedTransaction);
            if (result != OperatingError.Ok)
                return result;
            var transaction = acceptedTransaction.Transaction;
            if (transaction.GasLimit > GasMetering.DefaultBlockGasLimit ||
                transaction.GasLimit < GasMetering.DefaultTxTransferGasCost)
                return OperatingError.InvalidGasLimit;
            /* verify transaction via persister */
            var persister = _transactionPersisters[transaction.Type];
            if (persister == null)
                return OperatingError.UnsupportedTransaction;
            return persister.Verify(transaction);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public OperatingError VerifySignature(TransactionReceipt transaction, ECDSAPublicKey publicKey)
        {
            if (!_verifiedTransactions.ContainsKey(transaction.Hash))
                return _transactionVerifier.VerifyTransactionImmediately(transaction, publicKey)
                    ? OperatingError.Ok
                    : OperatingError.InvalidSignature;
            _verifiedTransactions.TryRemove(transaction.Hash, out _);
            return OperatingError.Ok;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public OperatingError VerifySignature(TransactionReceipt transaction, bool cacheEnabled = true)
        {
            if (!_verifiedTransactions.ContainsKey(transaction.Hash))
                return _transactionVerifier.VerifyTransactionImmediately(transaction, cacheEnabled)
                    ? OperatingError.Ok
                    : OperatingError.InvalidSignature;
            _verifiedTransactions.TryRemove(transaction.Hash, out _);
            return OperatingError.Ok;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public ulong CalcNextTxNonce(UInt160 from)
        {
            return _stateManager.LastApprovedSnapshot.Transactions.GetTotalTransactionCount(from);
        }
    }
}