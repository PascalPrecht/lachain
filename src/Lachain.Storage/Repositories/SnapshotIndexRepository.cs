﻿using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Lachain.Logger;
using Lachain.Storage.State;
using Lachain.Utility.Serialization;

namespace Lachain.Storage.Repositories
{
    public class SnapshotIndexRepository : ISnapshotIndexRepository
    {
        private readonly IRocksDbContext _dbContext;
        private readonly IStorageManager _storageManager;

        private static readonly ILogger<SnapshotIndexRepository> Logger =
            LoggerFactory.GetLoggerForClass<SnapshotIndexRepository>();

        public SnapshotIndexRepository(IRocksDbContext dbContext, IStorageManager storageManager)
        {
            _dbContext = dbContext;
            _storageManager = storageManager;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IBlockchainSnapshot GetSnapshotForBlock(ulong block)
        {
            return new BlockchainSnapshot(
                new BalanceSnapshot(_storageManager.GetState(
                    (uint) RepositoryType.BalanceRepository,
                    GetVersion((uint) RepositoryType.BalanceRepository, block))
                ),
                new ContractSnapshot(_storageManager.GetState(
                    (uint) RepositoryType.ContractRepository,
                    GetVersion((uint) RepositoryType.ContractRepository, block)
                )),
                new StorageSnapshot(_storageManager.GetState(
                    (uint) RepositoryType.StorageRepository,
                    GetVersion((uint) RepositoryType.StorageRepository, block))
                ),
                new TransactionSnapshot(_storageManager.GetState(
                    (uint) RepositoryType.TransactionRepository,
                    GetVersion((uint) RepositoryType.TransactionRepository, block))
                ),
                new BlockSnapshot(_storageManager.GetState(
                    (uint) RepositoryType.BlockRepository,
                    GetVersion((uint) RepositoryType.BlockRepository, block))
                ),
                new EventSnapshot(_storageManager.GetState(
                    (uint) RepositoryType.EventRepository,
                    GetVersion((uint) RepositoryType.EventRepository, block))
                ),
                new ValidatorSnapshot(_storageManager.GetState(
                    (uint) RepositoryType.ValidatorRepository,
                    GetVersion((uint) RepositoryType.ValidatorRepository, block))
                )
            );
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SaveSnapshotForBlock(ulong block, IBlockchainSnapshot snapshot)
        {
            SetVersion((uint) RepositoryType.BalanceRepository, block, snapshot.Balances.Version);
            SetVersion((uint) RepositoryType.ContractRepository, block, snapshot.Contracts.Version);
            SetVersion((uint) RepositoryType.StorageRepository, block, snapshot.Storage.Version);
            SetVersion((uint) RepositoryType.TransactionRepository, block, snapshot.Transactions.Version);
            SetVersion((uint) RepositoryType.BlockRepository, block, snapshot.Blocks.Version);
            SetVersion((uint) RepositoryType.EventRepository, block, snapshot.Events.Version);
            SetVersion((uint) RepositoryType.ValidatorRepository, block, snapshot.Validators.Version);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private ulong GetVersion(uint repository, ulong block)
        {
            var rawVersion = _dbContext.Get(EntryPrefix.SnapshotIndex.BuildPrefix(
                repository.ToBytes().Concat(block.ToBytes()).ToArray()
            ));
            if (rawVersion is null) throw new Exception($"snapshot for block: {block} is not found");
            return rawVersion.AsReadOnlySpan().ToUInt64();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void SetVersion(uint repository, ulong block, ulong version)
        {
            Logger.LogTrace($"Saved version {version} for {(RepositoryType) repository}");
            _dbContext.Save(
                EntryPrefix.SnapshotIndex.BuildPrefix(
                    repository.ToBytes().Concat(block.ToBytes()).ToArray()
                ),
                version.ToBytes()
            );
        }

        private void DeleteVersion(uint repository, ulong block, ulong version, RocksDbAtomicWrite batch)
        {
            Console.WriteLine($"Deleting version {version} for "
                + $"{(RepositoryType) repository} for block {block}");
            batch.Delete(
                EntryPrefix.SnapshotIndex.BuildPrefix(
                    repository.ToBytes().Concat(block.ToBytes()).ToArray()));
        }

        public void DeleteOldSnapshot(ulong depth, ulong totalBlocks)
        {
            Console.WriteLine($"Keeping latest {depth+1} snapshots from last approved snapshot" 
                + $"for blocks: {totalBlocks - depth} to {totalBlocks}");

            // saving nodes for recent (depth + 1) snapshots temporarily
            // so that all remaining nodes can be deleted
            UpdateNodeIdToBatch(totalBlocks, depth, true);

            // deleting all nodes that are not reachable from recent (depth+1) snapshots
            ulong deletedNodes = 0;
            Console.WriteLine("Deleting nodes from DB that are not reachable from recent snapshots");
            for(ulong block = 0 ; block < totalBlocks - depth; block++)
            {
                var blockchainSnapshot = GetSnapshotForBlock(block);
                var snapshots = blockchainSnapshot.GetAllSnapshot();
                foreach(var snapshot in snapshots)
                {
                    Console.WriteLine($"Deleting nodes of "
                         + $"{(RepositoryType) snapshot.RepositoryId} for block {block}");
                    var batch = new RocksDbAtomicWrite(_dbContext);
                    var count = snapshot.DeleteSnapshot(block, batch);
                    deletedNodes += count;
                    DeleteVersion(snapshot.RepositoryId, block, snapshot.Version, batch);
                    batch.Commit();
                    Console.WriteLine($"Deleted {count} nodes of "
                         + $"{(RepositoryType) snapshot.RepositoryId} for block {block}");
                }
            }
            Console.WriteLine($"Deleted {deletedNodes} nodes from DB in total");

            // deleting temporary nodes
            UpdateNodeIdToBatch(totalBlocks, depth, false);
        }

        public void UpdateNodeIdToBatch(ulong totalBlocks, ulong depth, bool save)
        {
            (string Doing, string Done) action = save ? ("Saving" , "Saved") : ("Deleting" , "Deleted");
            Console.WriteLine($"{action.Doing} nodeId");
            ulong usefulNodes = 0;
            for(var block = totalBlocks - depth; block <= totalBlocks; block++)
            {
                var blockchainSnapshot = GetSnapshotForBlock(block);
                var snapshots = blockchainSnapshot.GetAllSnapshot();
                foreach(var snapshot in snapshots)
                {
                    Console.WriteLine($"{action.Doing} nodeId of "
                         + $"{(RepositoryType) snapshot.RepositoryId} for block {block}");
                    var batch = new RocksDbAtomicWrite(_dbContext);
                    var count = snapshot.UpdateNodeIdToBatch(save, batch);
                    usefulNodes += count;
                    batch.Commit();
                    Console.WriteLine($"{action.Done} {count} nodeId of "
                         + $"{(RepositoryType) snapshot.RepositoryId} for block {block}");
                }
            }
            Console.WriteLine($"{action.Done} {usefulNodes} nodeId in total");
        }
    }
}