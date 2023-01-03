using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Lachain.Crypto;
using Lachain.Logger;
using Lachain.Proto;
using Lachain.Storage.DbCompact;
using Lachain.Storage.Trie;
using Lachain.Utility;
using Lachain.Utility.Utils;

namespace Lachain.Storage.State
{
    /*
        Balance Snapshot is basically a HMAT (trie) and it stores the balance of all the address. 
        You can think of it as a key-value storage of (address -> balance).
    */
    public class NoVerificationBalanceSnapshot : IBalanceSnapshot
    {
        private static readonly ILogger<NoVerificationBalanceSnapshot> Logger
            = LoggerFactory.GetLoggerForClass<NoVerificationBalanceSnapshot>();
        private readonly IStorageState _state;
        private static readonly ICrypto Crypto = CryptoProvider.GetCrypto();

        public NoVerificationBalanceSnapshot(IStorageState state)
        {
            _state = state;
        }
        
        public IDictionary<ulong,IHashTrieNode> GetState()
        {
            return _state.GetAllNodes();
        }

        public bool IsTrieNodeHashesOk()
        {
            return _state.IsNodeHashesOk();
        }

        public ulong SetState(ulong root, IDictionary<ulong, IHashTrieNode> allTrieNodes)
        {
            return _state.InsertAllNodes(root, allTrieNodes);
        }

        public ulong Version => _state.CurrentVersion;

        public uint RepositoryId => _state.RepositoryId;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Money GetBalance(UInt160 owner)
        {
            var key = EntryPrefix.BalanceByOwnerAndAsset.BuildPrefix(owner);
            var value = _state.Get(key);
            var balance = value?.ToUInt256() ?? UInt256Utils.Zero;
            return new Money(balance);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Money GetSupply()
        {
            var key = EntryPrefix.TotalSupply.BuildPrefix();
            var value = _state.Get(key);
            var supply = value?.ToUInt256() ?? UInt256Utils.Zero;
            return new Money(supply);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void SetBalance(UInt160 owner, Money value)
        {
            var key = EntryPrefix.BalanceByOwnerAndAsset.BuildPrefix(owner);
            _state.AddOrUpdate(key, value.ToUInt256().ToBytes());
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void SetSupply(Money value)
        {
            var key = EntryPrefix.TotalSupply.BuildPrefix();
            _state.AddOrUpdate(key, value.ToUInt256().ToBytes());
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private Money AddBalance(UInt160 owner, Money value, bool increaseSupply = false)
        {
            var balance = GetBalance(owner);
            balance += value;
            SetBalance(owner, balance);
            if (increaseSupply)
            {
                var supply = GetSupply();
                supply += value;
                SetSupply(supply);
            }

            return balance;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private Money SubBalance(UInt160 owner, Money value)
        {
            var balance = GetBalance(owner);
            balance -= value;
            SetBalance(owner, balance);
            return balance;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool TransferBalance(
            UInt160 from, UInt160 to, Money value, TransactionReceipt receipt, bool checkSignature, bool useNewChainId
        )
        {
            var availableBalance = GetBalance(from);
            if (availableBalance.CompareTo(value) < 0)
                return false;
            if (checkSignature)
            {
                // never check signature because this snapshot bypasses verification
            }
            SubBalance(from, value);
            AddBalance(to, value);
            return true;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool TransferAllowance(UInt160 from, UInt160 to, Money value, Money allowance)
        {
            if (allowance.CompareTo(value) < 0) // not enough allowance
                return false;
            var availableBalance = GetBalance(from);
            if (availableBalance.CompareTo(value) < 0)
                return false;
            SubBalance(from, value);
            AddBalance(to, value);
            return true;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Money MintLaToken(UInt160 address, Money value)
        {
            return AddBalance(address, value, true);
        }
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Money GetAllowedSupply()
        {
            var key = EntryPrefix.AllowedSupply.BuildPrefix();
            var value = _state.Get(key);
            var supply = value?.ToUInt256() ?? UInt256Utils.Zero;
            return new Money(supply);
        }
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SetAllowedSupply(Money value)
        {
            var key = EntryPrefix.AllowedSupply.BuildPrefix();
            _state.AddOrUpdate(key, value.ToUInt256().ToBytes());
        }
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public UInt160 GetMinter()
        {
            var key = EntryPrefix.MinterAddress.BuildPrefix();
            var value = _state.Get(key);
        
            if (value == null)
                return UInt160Utils.Zero;
            
            var address = value?.ToUInt160() ?? UInt160Utils.Zero;
            return address;
        }
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SetMinter(UInt160 value)
        {
            var key = EntryPrefix.MinterAddress.BuildPrefix();
            _state.AddOrUpdate(key, value.ToBytes());
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Money RemoveCollectedFees(Money fee, TransactionReceipt receipt)
        {
            if (!receipt.Transaction.From.Equals(UInt160Utils.Zero))
                throw new Exception($"Non system contract transaction {receipt.Hash.ToHex()} requests RemoveCollectedFees");
            return SubBalance(SystemContractAddresses.GovernanceContract, fee);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool TransferContractBalance(UInt160 from, UInt160 to, Money value)
        {
            var availableBalance = GetBalance(from);
            if (availableBalance.CompareTo(value) < 0)
                return false;
            SubBalance(from, value);
            AddBalance(to, value);
            return true;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool TransferSystemContractBalance(
            UInt160 from, UInt160 to, Money value, TransactionReceipt receipt, bool checkVerification
        )
        {
            if (checkVerification)
            {
                // never check signature because this snapshot bypasses verification
            }
            var availableBalance = GetBalance(from);
            if (availableBalance.CompareTo(value) < 0)
                return false;
            SubBalance(from, value);
            AddBalance(to, value);
            return true;
        }

        public void Commit(RocksDbAtomicWrite batch)
        {
            throw new NotImplementedException(
                "NoVerificationBalanceSnapshot cannot commit to state because we bypass verification, so we cannot change state"
            );
        }
        public void SetCurrentVersion(ulong root)
        {
            _state.SetCurrentVersion(root);
        }
        public void ClearCache()
        {
            _state.ClearCache();
        }
        public UInt256 Hash => _state.Hash;

        public ulong SaveNodeId(IDbShrinkRepository _repo)
        {
            return _state.SaveNodeId(_repo);
        }

    }
}