using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Lachain.Crypto;
using Lachain.Logger;
using Lachain.Networking.Hub;
using Lachain.Proto;
using Lachain.Storage.Repositories;
using Lachain.Utility.Utils;
using Prometheus;

namespace Lachain.Networking.PeerFault
{
    public class PeerBanManager : IPeerBanManager
    {
        private static readonly ILogger<PeerBanManager> Logger = LoggerFactory.GetLoggerForClass<PeerBanManager>();
        private static readonly Counter BanForPenaltyCounter = Metrics.CreateCounter(
            "lachain_peer_banned_for_penalty_count",
            "Number of times a peer was banned for penalty",
            "peer"
        );
        private static readonly Gauge BanByPenaltyCycle = Metrics.CreateGauge(
            "lachain_peer_banned_cycle",
            "Latest cycle where a peer was banned for penalty",
            new GaugeConfiguration
            {
                LabelNames = new[] {"peer", "penalties"}
            }
        );
        private ulong _cycle = 0;
        // ban this peer for 3 cycles: this _cycle, _cycle + 1 and _cycle + 2
        // this way even if he can become validator, will not get any attendance
        // from me for 2 cycles, thus increasing chances of getting penalty
        public const ulong MaxCycleCountToBan = 3;
        private readonly IPeerBanRepository _repository;
        private readonly INetworkManager _networkManager;
        private readonly ConcurrentDictionary<ECDSAPublicKey, (ulong fromCycle, ulong toCycle)> _bannedPeers
            = new ConcurrentDictionary<ECDSAPublicKey, (ulong, ulong)>();
        public event EventHandler<(byte[] publicKey, ulong penalties)>? OnPeerBanned;
        public PeerBanManager(IPeerBanRepository repository, INetworkManager networkManager)
        {
            _networkManager = networkManager;
            _repository = repository;
            _networkManager.OnAdvanceEra += AdvanceEra;
            _networkManager.OnWorkerCreated += RegisterPeer;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RestoreState()
        {
            _cycle = _repository.GetLowestCycle();
            for (var cycle = _cycle; cycle < _cycle + MaxCycleCountToBan; cycle++)
            {
                var peerList = _repository.GetBannedPeers(cycle);
                for (int i = 0 ; i < peerList.Length; i += CryptoUtils.PublicKeyLength)
                {
                    var publicKey = CryptoUtils.ToPublicKey(peerList.Skip(i).Take(CryptoUtils.PublicKeyLength).ToArray());
                    ulong from = cycle;
                    ulong to = cycle;
                    if (_bannedPeers.TryGetValue(publicKey, out var value))
                    {
                        from = value.fromCycle;
                    }
                    else
                    {
                        _networkManager.BanPeer(publicKey.EncodeCompressed());
                    }
                    _bannedPeers[publicKey] = (from, to);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void AdvanceEra(object? sender, ulong era)
        {
            var cycle = NetworkManagerBase.CycleNumber(era);
            if (cycle < _cycle)
            {
                throw new Exception($"We are on cycle {_cycle} but advancing to cycle {cycle}");
            }
            if (cycle > _cycle)
            {
                Logger.LogTrace($"Removing banned peer list of cycle {_cycle}");
                var peerList = _repository.GetBannedPeers(_cycle);
                for (int i = 0 ; i < peerList.Length; i += CryptoUtils.PublicKeyLength)
                {
                    var publicKey = CryptoUtils.ToPublicKey(peerList.Skip(i).Take(CryptoUtils.PublicKeyLength).ToArray());
                    if (_bannedPeers.TryGetValue(publicKey, out var value))
                    {
                        if (value.toCycle < cycle)
                        {
                            _bannedPeers.TryRemove(publicKey, out var _);
                            _networkManager.RemoveFromBanList(publicKey.EncodeCompressed());
                            Logger.LogTrace($"Removed peer {publicKey.ToHex()} from ban list after cycle {_cycle}");
                        }
                    }
                }
                _repository.RemoveCycle(_cycle);
                _cycle = cycle;
            }
        }

        private void RegisterPeer(object? sender, ClientWorker peer)
        {
            peer._penaltyHandler.OnTooManyPenalty += BanPeerForPenalty;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void BanPeer(byte[] publicKey)
        {
            ulong start = _cycle;
            var ecdsaPubKey = CryptoUtils.ToPublicKey(publicKey);
            if (_bannedPeers.TryGetValue(ecdsaPubKey, out var value))
            {
                start = value.toCycle + 1;
            }
            else
            {
                _networkManager.BanPeer(publicKey);
            }
            for (var cycle = start; cycle < _cycle + MaxCycleCountToBan; cycle++)
            {
                _repository.AddBannedPeer(cycle, publicKey);
                Logger.LogTrace($"Banned peer {publicKey.ToHex()} for cycle {cycle}");
            }
            _bannedPeers[ecdsaPubKey] = (_cycle, _cycle + 2);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void BanPeerForPenalty(object? sender, (byte[] publicKey, ulong penalties) @event)
        {
            Task.Factory.StartNew(() =>
            {
                var (publicKey, penalties) = @event;
                BanForPenaltyCounter.WithLabels(publicKey.ToHex()).Inc();
                BanByPenaltyCycle.WithLabels(publicKey.ToHex(), penalties.ToString()).Set(_cycle);
                BanPeer(publicKey);
                Logger.LogTrace($"Banned peer {publicKey.ToHex()} for {penalties} penalties during cycle {_cycle}");
                OnPeerBanned?.Invoke(this, (publicKey, penalties));
            }, TaskCreationOptions.LongRunning);
        }
    }
}