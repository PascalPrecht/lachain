namespace Lachain.Core.Network.FastSynchronizerBatch
{
    public enum RequestType : byte
    {
        BlocksRequest = 0,
        NodesRequest = 1,
        CheckpointBlockRequest = 2,
        CheckpointStateHashRequest = 3,
    }
}