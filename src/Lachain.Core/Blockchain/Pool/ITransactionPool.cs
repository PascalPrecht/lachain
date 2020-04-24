﻿using System.Collections.Generic;
using Lachain.Core.Blockchain.OperationManager;
using Lachain.Proto;

namespace Lachain.Core.Blockchain.Pool
{
    public interface ITransactionPool
    {
        IReadOnlyDictionary<UInt256, TransactionReceipt> Transactions { get; }

        TransactionReceipt? GetByHash(UInt256 hash);

        void Restore();

        OperatingError Add(Transaction transaction, Signature signature);
        
        OperatingError Add(TransactionReceipt receipt);
        
        IReadOnlyCollection<TransactionReceipt> Peek(int txsToLook, int txsToTake);

        void Relay(IEnumerable<TransactionReceipt> receipts);
        
        uint Size();
        
        void Delete(UInt256 transactionHash);

        void Clear();
        ulong? GetMaxNonceForAddress(UInt160 address);
        ulong GetNextNonceForAddress(UInt160 address);
    }
}