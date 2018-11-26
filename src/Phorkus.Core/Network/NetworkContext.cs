﻿using System;
using System.Collections.Concurrent;
using Phorkus.Core.Blockchain;
using Phorkus.Core.Config;
using Phorkus.Proto;

namespace Phorkus.Core.Network
{
    public class NetworkContext : INetworkContext
    {
        public ConcurrentDictionary<PeerAddress, IRemotePeer> ActivePeers { get; }
            = new ConcurrentDictionary<PeerAddress, IRemotePeer>();
        
        public Node LocalNode { get; }
        
        public NetworkContext(IConfigManager configManager, IBlockchainContext blockchainContext)
        {
            var networkConfig = configManager.GetConfig<NetworkConfig>("network");
            LocalNode = new Node
            {
                Version = 0,
                Timestamp = (ulong) DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                Services = 0,
                Port = networkConfig.Port,
                Address = "localhost",
                Nonce = (uint) new Random().Next(1 << 30),
                BlockHeight = blockchainContext.CurrentBlockHeaderHeight,
                Agent = "Phorkus-v0.0"
            };
        }
    }
}