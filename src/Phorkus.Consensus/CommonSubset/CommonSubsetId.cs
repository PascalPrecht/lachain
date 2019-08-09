﻿using System;
using System.Collections.Generic;
using System.Linq;
using Phorkus.Consensus.HoneyBadger;

namespace Phorkus.Consensus.CommonSubset
{
    public class CommonSubsetId : IProtocolIdentifier
    {
        public CommonSubsetId(HoneyBadgerId honeyBadgerId)
        {
            Era = honeyBadgerId.Era;
        }
        public bool Equals(IProtocolIdentifier other)
        {
            throw new NotImplementedException();
        }

        public ulong Era { get; }
        public IEnumerable<byte> ToByteArray()
        {
            throw new NotImplementedException();
        }
    }
}