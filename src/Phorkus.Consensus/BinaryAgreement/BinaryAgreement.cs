﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Phorkus.Consensus.CommonCoin;
using Phorkus.Consensus.Messages;
using Phorkus.Utility.Utils;

namespace Phorkus.Consensus.BinaryAgreement
{
    public class BinaryAgreement : AbstractProtocol
    {
        private readonly BinaryAgreementId _agreementId;
        private bool? _result;
        private ResultStatus _requested;

        private long _currentEpoch;
        private bool _estimate;
        private BoolSet _currentValues;
        private bool _wasRepeat;
        private long _resultEpoch;

        private readonly Dictionary<long, bool> _coins = new Dictionary<long, bool>();
        private readonly Dictionary<long, BoolSet> _binaryBroadcastsResults = new Dictionary<long, BoolSet>();

        public override IProtocolIdentifier Id => _agreementId;

        public BinaryAgreement(BinaryAgreementId agreementId, IWallet wallet, IConsensusBroadcaster broadcaster)
        : base(wallet, broadcaster)
        {
            _agreementId = agreementId;
            _requested = ResultStatus.NotRequested;
            _currentEpoch = 0;
            _wasRepeat = false;
        }

        private void CheckResult()
        {
            if (_result == null) return;
            if (_requested == ResultStatus.Requested)
            {
                _broadcaster.InternalResponse(new ProtocolResult<BinaryAgreementId, bool>(_agreementId, (bool) _result));
                _requested = ResultStatus.Sent;
                SetResult();
                Console.Error.WriteLine($"Player {GetMyId()} at {_agreementId}: made result succ at Ep={_currentEpoch}");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void TryProgressEpoch()
        {
            CheckResult();
            while (_result == null || !_wasRepeat)
            {
                if (_currentEpoch % 2 == 0)
                {
                    // epoch mod 2 = 0 -> we have not yet initiated BB
                    
                    if (_currentEpoch != 0 && !_coins.ContainsKey(_currentEpoch - 1))
                    {
//                        throw new Exception($"Player {GetMyId()}: can not progress epoch, blocked, coin not present!.");
                        Console.Error.WriteLine($"Player {GetMyId()}: can not progress epoch, blocked, coin (Ep={_currentEpoch - 1}) not present!.");
                        return; // we cannot progress since coin is not tossed and estimate is not correct
                    }
                    else
                    {
                        Console.Error.WriteLine($"Player {GetMyId()}: epoch progressed, coin (Ep={_currentEpoch - 1} is present.");
                    }
                    

                    // we have right to calculate new estimate and proceed
                    if (_currentEpoch != 0)
                    {
                        var s = _coins[_currentEpoch - 1];
                        _estimate = _currentValues.Values().First();
                        
                        if (_currentValues.Count() == 1 && _result == null)
                        {
                                if (_estimate == s)
                                {
                                    // we are winners!!!!!!!!!!!!!!1
                                    _resultEpoch = _currentEpoch;
                                    _result = _estimate;
                                    CheckResult();
                                    Console.Error.WriteLine($"Player {GetMyId()} at {_agreementId}: result = {_result} achieved at Ep={_currentEpoch}");
                                }
                        } else if (_result == s)
                        {
                            if (_currentEpoch > _resultEpoch)
                            {
                                _wasRepeat = true;
                                CheckResult();
                            }
                        }
                        else
                        {
                            _estimate = s;
                        }
                    }

                    if (_result != null)
                        _estimate = _result.Value;

                    // here we start new BB assuming that current estimate is correct
                    var broadcastId = new BinaryBroadcastId(_agreementId.Era, _agreementId.AssociatedValidatorId, _currentEpoch);
                    _broadcaster.InternalRequest(
                        new ProtocolRequest<BinaryBroadcastId, bool>(Id, broadcastId, _estimate)
                    );
                    _currentEpoch += 1;
                }
                else
                {
                    // epoch mod 2 = 1 -> we have not yet tossed coin
                    if (!_binaryBroadcastsResults.ContainsKey(_currentEpoch - 1))
                    {
//                        throw new Exception($"Player {GetMyId()}: can not progress epoch, blocked, BB not present!.");
                        Console.Error.WriteLine($"Player {GetMyId()}: can not progress epoch, blocked, BB (Ep={_currentEpoch-1}) not present!.");
                        return; // we cannot progress since BB is not completed
                    }
                    Console.Error.WriteLine($"Player {GetMyId()}: epoch progressed, BB (Ep={_currentEpoch - 1} is present.");
                    
                    _currentValues = _binaryBroadcastsResults[_currentEpoch - 1];
                    var coinId = new CoinId(_agreementId.Era, _agreementId.AssociatedValidatorId, _currentEpoch);
                    _broadcaster.InternalRequest(new ProtocolRequest<CoinId, object>(Id, coinId, null));
                    Console.Error.WriteLine($"Player {GetMyId()}: send request for coin {coinId}");
                    _currentEpoch += 1;
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public override void ProcessMessage(MessageEnvelope envelope)
        {
            if (envelope.External)
            {
                throw new InvalidOperationException("Binary agreement should not receive external messages");
            }

            var message = envelope.InternalMessage;

            switch (message)
            {
                case ProtocolRequest<BinaryAgreementId, bool> agreementRequested:
                    if (_currentEpoch != 0 || _requested != ResultStatus.NotRequested)
                    {
                        break;
                        // todo fix back or add some logic to handle parents fault
                        throw new InvalidOperationException("Cannot propose value: protocol is already running");
                    }

                    _requested = ResultStatus.Requested;
                    _estimate = agreementRequested.Input;
                    TryProgressEpoch();
                    break;
                case ProtocolResult<BinaryAgreementId, bool> _:
//                    Terminated = true;
                    break;
                case ProtocolResult<BinaryBroadcastId, BoolSet> broadcastCompleted:
                {
                    _binaryBroadcastsResults[broadcastCompleted.Id.Epoch] = broadcastCompleted.Result;
                    TryProgressEpoch();
                    return;
                }
                case ProtocolResult<CoinId, bool> coinTossed:
                    _coins[coinTossed.Id.Epoch] = coinTossed.Result;
                    TryProgressEpoch();
                    return;
                default:
                    throw new InvalidOperationException($"Cannot handle message of type {message.GetType()}");
            }
        }
    }
}