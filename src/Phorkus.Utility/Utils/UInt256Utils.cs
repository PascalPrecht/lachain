﻿using System;
using System.Linq;
using System.Numerics;
using Google.Protobuf;
using Phorkus.Proto;

namespace Phorkus.Utility.Utils
{
    public static class UInt256Utils
    {
        /// <summary>
        /// empty UInt256 number is 32 empty bytes 
        /// </summary>
        public static readonly UInt256 Zero = new byte[32].ToUInt256();

        public static bool IsZero(this UInt256 value)
        {
            return Zero.Equals(value);
        }
        
        public static BigInteger ToBigInteger(this UInt256 value)
        {
            return new BigInteger(value.Buffer.ToByteArray().Concat(new byte[] {0}).ToArray());
        }

        public static UInt256 ToUInt256(this BigInteger value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            var bytes = value.ToByteArray();
            if (bytes.Length > 33 || bytes.Length == 33 && bytes[32] != 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            return bytes.Take(32).Concat(new byte[Math.Max(32 - bytes.Length, 0)]).ToArray().ToUInt256();
        }

        public static Money ToMoney(this UInt256 value)
        {
            return new Money(value.ToBigInteger());
        }

        public static UInt256 ToUInt256(this byte[] buffer)
        {
            if (buffer.Length != 32)
                throw new ArgumentOutOfRangeException(nameof(buffer));
            return new UInt256
            {
                Buffer = ByteString.CopyFrom(buffer)
            };
        }
    }
}