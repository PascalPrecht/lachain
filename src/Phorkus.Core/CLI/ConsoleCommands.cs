using System.Collections.Generic;
using System.Linq;
using Phorkus.Core.Blockchain;
using Phorkus.Core.Blockchain.OperationManager;
using Phorkus.Core.Utils;
using Phorkus.Crypto;
using Phorkus.Proto;
using Phorkus.Storage.RocksDB.Repositories;
using Phorkus.Storage.State;

namespace Phorkus.Core.CLI
{
    public class ConsoleCommands : IConsoleCommands
    {
        private const uint AddressLength = 20;
        private const uint TxLength = 32;
        
        private readonly IGlobalRepository _globalRepository;
        private readonly IValidatorManager _validatorManager;
        private readonly ITransactionPool _transactionPool;
        private readonly ITransactionBuilder _transactionBuilder;
        private readonly ITransactionManager _transactionManager;
        private readonly IBlockManager _blockManager;
        private readonly ICrypto _crypto;
        private readonly IBlockchainStateManager _blockchainStateManager;
        private readonly KeyPair _keyPair;

        public ConsoleCommands(
            IGlobalRepository globalRepository,
            ITransactionBuilder transactionBuilder,
            ITransactionPool transactionPool,
            ITransactionManager transactionManager,
            IBlockManager blockManager,
            IValidatorManager validatorManager,
            IBlockchainStateManager blockchainStateManager,
            ICrypto crypto,
            KeyPair keyPair)
        {
            _blockManager = blockManager;
            _globalRepository = globalRepository;
            _transactionBuilder = transactionBuilder;
            _transactionPool = transactionPool;
            _transactionManager = transactionManager;
            _validatorManager = validatorManager;
            _blockchainStateManager = blockchainStateManager;
            _crypto = crypto;
            _keyPair = keyPair;
        }

        private static bool IsValidHexString(IEnumerable<char> hexString)
        {
            return hexString.Select(currentCharacter =>
                currentCharacter >= '0' && currentCharacter <= '9' ||
                currentCharacter >= 'a' && currentCharacter <= 'f' ||
                currentCharacter >= 'A' && currentCharacter <= 'F').All(isHexCharacter => isHexCharacter);
        }

        private static string EraseHexPrefix(string hexString)
        {
            if (hexString.StartsWith("0x"))
                hexString = hexString.Substring(2);
            return hexString;
        }

        /*
         * GetTransaction
         * blockHash, UInt256
        */
        public SignedTransaction GetTransaction(string[] arguments)
        {
            if (arguments.Length != 2 || arguments[1].Length != TxLength)
                return null;
            arguments[1] = EraseHexPrefix(arguments[1]);
            return !IsValidHexString(arguments[1])
                ? null
                : _transactionManager.GetByHash(HexUtils.HexToUInt256(arguments[1]));
        }

        /*
         * GetBlock
         * blockHash, UInt256
        */
        public Block GetBlock(string[] arguments)
        {
            if (arguments.Length != 2)
                return null;
            var value = EraseHexPrefix(arguments[1]);
            if (ulong.TryParse(value, out var blockHeight))
                return _blockManager.GetByHeight(blockHeight);
            return _blockManager.GetByHash(arguments[1].HexToUInt256());
        }

        /*
         * GetBalance
         * address, UInt160
         * asset, UInt160
        */
        public UInt256 GetBalance(string[] arguments)
        {
            if (arguments.Length != 3 || arguments[1].Length != AddressLength)
                return null;
            arguments[1] = EraseHexPrefix(arguments[1]);
            if (!IsValidHexString(arguments[1]))
                return null;
            var asset = _blockchainStateManager.LastApprovedSnapshot.Assets.GetAssetByName(arguments[2]);
            if (asset == null)
                return null;
            return _blockchainStateManager.LastApprovedSnapshot.Balances
                .GetAvailableBalance(arguments[1].HexToUInt160(), asset.Hash).ToUInt256();
        }

        /*
         * GetTransactionPool
        */
        public IEnumerable<SignedTransaction> GetTransactionPool(string[] arguments)
        {
            return _transactionPool.Transactions.Values;
        }

        /*
         * SignBlock
         * blockHash, UInt256
        */
        public Signature SignBlock(string[] arguments)
        {
            if (arguments.Length != 2 || arguments[1].Length != TxLength)
                return null;

            arguments[1] = EraseHexPrefix(arguments[1]);
            if (!IsValidHexString(arguments[1]))
                return null;
            var block = _blockManager.GetByHash(arguments[1].HexToUInt256());
            return _blockManager.Sign(block.Header, _keyPair);
        }
        
     
        
        /*
         * SendTransaction
         * from, UInt160,
         * to, UInt160,
         * assetName, string
         * value, UInt256,
         * fee, UInt256
        */
        public SignedTransaction SendTransaction(string[] arguments)
        {
            var from = HexUtils.HexToUInt160(arguments[1]);
            var to = HexUtils.HexToUInt160(arguments[2]);
            var asset = _blockchainStateManager.LastApprovedSnapshot.Assets.GetAssetByName(arguments[3]);
            var value = HexUtils.HexToUInt256(arguments[4]);
            var fee = HexUtils.HexToUInt256(arguments[5]);
            var tx = _transactionBuilder.ContractTransaction(from, to, asset, value, fee, null);
            var signedTx = _transactionManager.Sign(tx, _keyPair);
            _transactionPool.Add(signedTx);
            return signedTx;
        }

     /*
         public Signature SignTransaction(string[] arguments)
         {
             if (arguments.Length != 2 || arguments[1].Length != TxLength)
             {
                 return null;
             }
    
             arguments[1] = EraseHexPrefix(arguments[1]);
             if (!IsValidHexString(arguments[1]))
             {
                 return null;
             }

             Block block = _transactionPool.GetByHash(HexUtils.HexToUInt256(arguments[1]));
             return _blockManager.Sign(block.Header, _keyPair);
         }*/
    }
}