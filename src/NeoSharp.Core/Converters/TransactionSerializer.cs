﻿using System;
using System.IO;
using NeoSharp.BinarySerialization;
using NeoSharp.Core.Caching;
using NeoSharp.Core.Models;

namespace NeoSharp.Core.Converters
{
    public class TransactionSerializer : IBinaryCustomSerializable
    {
        /// <summary>
        /// Cache
        /// </summary>
        private static ReflectionCache<byte> Cache;
        
        public object Deserialize(IBinarySerializer deserializer, BinaryReader reader, Type type, BinarySerializerSettings settings = null)
        {
            if (Cache is null)
                Cache = ReflectionCache<byte>.CreateFromEnum<TransactionType>();

            byte txType = reader.ReadByte();
//            Console.WriteLine("TX TYPE: " + txType);
            var tx = Cache.CreateInstance<Transaction>(txType);
            
            tx?.Deserialize(deserializer, reader, settings);

            return tx;
        }

        public int Serialize(IBinarySerializer serializer, BinaryWriter writer, object value, BinarySerializerSettings settings = null)
        {
            var tx = (Transaction)value;

            return tx.Serialize(serializer, writer, settings);
        }
    }
}