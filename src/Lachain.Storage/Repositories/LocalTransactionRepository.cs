using System;
using System.Collections.Generic;
using System.Linq;
using Lachain.Logger;
using Lachain.Proto;
using Lachain.Utility.Utils;
using Nethereum.Util;

namespace Lachain.Storage.Repositories
{
    public class LocalTransactionRepository : ILocalTransactionRepository
    {
        private static readonly ILogger<LocalTransactionRepository> Logger =
            LoggerFactory.GetLoggerForClass<LocalTransactionRepository>();
        
        private readonly IRocksDbContext _rocksDbContext;
        private UInt160[] _watchAddresses;

        public LocalTransactionRepository(IRocksDbContext rocksDbContext)
        {
            _rocksDbContext = rocksDbContext ?? throw new ArgumentNullException(nameof(rocksDbContext));
            _watchAddresses = new UInt160[] {};
        }

        public void SetWatchAddress(UInt160 address)
        {
            _watchAddresses = new[] {address};
        }

        public void SaveState(byte[] state)
        {
            var key = EntryPrefix.LocalTransactionsState.BuildPrefix();
            _rocksDbContext.Save(key, state);
        }

        public void TryAddTransaction(TransactionReceipt receipt)
        {
            string[] signatures =
            {
                "transfer(address,uint256)",
                "transferFrom(address,address,uint256)",
                "mint(address,uint256)",
                "burn(address,uint256)"
            };

            var decoder = new ContractDecoderLtr(receipt.Transaction.Invocation.ToArray());
            object[] decodedRes = Array.Empty<object>();

            foreach (var signature in signatures)
            {
                try
                {
                    decodedRes = decoder.Decode(signature);

                    if (decodedRes.Length == 3)
                    {
                        if (_watchAddresses.Count(addr =>
                            decodedRes[0] is UInt160 fromAddr && fromAddr.Equals(addr) ||
                            decodedRes[1] is UInt160 toAddr && toAddr.Equals(addr)) <= 0) continue;

                        var temp = LoadState();
                        SaveState(temp.Concat(receipt.Hash.ToBytes()).ToArray());
                    }

                    else if (decodedRes.Length == 2)
                    {
                        if (_watchAddresses.Count(addr =>
                            decodedRes[0] is UInt160 fromAddr && fromAddr.Equals(addr)) <= 0) continue;

                        var temp = LoadState();
                        SaveState(temp.Concat(receipt.Hash.ToBytes()).ToArray());
                    }
                    else
                    {
                        if (_watchAddresses.Count(addr =>
                            addr.Equals(receipt.Transaction.To) || addr.Equals(receipt.Transaction.From)) <= 0) return;

                        var data = LoadState();
                        SaveState(data.Concat(receipt.Hash.ToBytes()).ToArray());
                    }
                }
                catch (Exception e)
                {
                    Logger.LogError($"Exception in adding TryAddTransaction: {e}");
                }
            }
        }

        public byte[] LoadState()
        {
            var key = EntryPrefix.LocalTransactionsState.BuildPrefix();
            var res= _rocksDbContext.Get(key);
            return res ?? new byte[] {};
        }

        public UInt256[] GetTransactionHashes(ulong limit)
        {
            var results = new List<UInt256>();
            var key = EntryPrefix.LocalTransactionsState.BuildPrefix();
            var data = _rocksDbContext.Get(key);
            for (var i = data.Length; i > 0 && i > data.Length - (int) limit * 32; i -= 32)
            {
                results.Add(data.Slice(i - 32, i).ToArray().ToUInt256());
            }

            return results.ToArray();
        }
    }
}