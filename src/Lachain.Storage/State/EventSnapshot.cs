﻿using System;
using System.Linq;
using Google.Protobuf;
using Lachain.Proto;
using Lachain.Utility.Serialization;
using Lachain.Utility.Utils;
using Lachain.Storage.Trie;
using System.Collections.Generic;
using Lachain.Utility;


namespace Lachain.Storage.State
{
    public class EventSnapshot : IEventSnapshot
    {
        private readonly IStorageState _state;

        public EventSnapshot(IStorageState state)
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

        public void Commit(RocksDbAtomicWrite batch)
        {
            _state.Commit(batch);
        }

        public UInt256 Hash => _state.Hash;

        public void AddEvent(EventObject eventObj)
        {
            var @event = eventObj._event;
            var total = GetTotalTransactionEvents(@event!.TransactionHash ?? UInt256Utils.Zero);
            @event.Index = total;
            _state.AddOrUpdate(
                EntryPrefix.EventCountByTransactionHash.BuildPrefix(@event.TransactionHash ?? UInt256Utils.Zero),
                (total + 1).ToBytes().ToArray()
            );
            var prefix = EntryPrefix.EventByTransactionHashAndIndex.BuildPrefix(@event.TransactionHash?? UInt256Utils.Zero, total);
            _state.AddOrUpdate(prefix, @event.ToByteArray());
            if(eventObj._topics != null)
            {
                prefix = EntryPrefix.EventTopicsByTransactionHashAndIndex.BuildPrefix(@event.TransactionHash?? UInt256Utils.Zero, total);
                _state.AddOrUpdate(prefix, eventObj._topics.TransactionHashListToByteArray());
            }
        }

        public EventObject GetEventByTransactionHashAndIndex(UInt256 transactionHash, uint eventIndex)
        {
            var prefix = EntryPrefix.EventByTransactionHashAndIndex.BuildPrefix(transactionHash, eventIndex);
            var raw = _state.Get(prefix);
            
            prefix = EntryPrefix.EventTopicsByTransactionHashAndIndex.BuildPrefix(transactionHash, eventIndex);
            var topics = _state.Get(prefix);
            return new EventObject(
                raw is null ? null : Event.Parser.ParseFrom(raw),
                topics is null ? null : topics.ByteArrayToTransactionHashList().ToList()
            );
        }

        public uint GetTotalTransactionEvents(UInt256 transactionHash)
        {
            var raw = _state.Get(EntryPrefix.EventCountByTransactionHash.BuildPrefix(transactionHash));
            return raw?.AsReadOnlySpan().ToUInt32() ?? 0u;
        }
        
        public void SetCurrentVersion(ulong root)
        {
            _state.SetCurrentVersion(root);
        }
        public void ClearCache()
        {
            _state.ClearCache();
        }
    }
}