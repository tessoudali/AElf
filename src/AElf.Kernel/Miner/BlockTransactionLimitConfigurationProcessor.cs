using System.Threading.Tasks;
using AElf.Kernel.Configuration;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace AElf.Kernel.Miner
{
    public class BlockTransactionLimitConfigurationProcessor : IConfigurationProcessor, ITransientDependency
    {
        private readonly IBlockTransactionLimitProvider _blockTransactionLimitProvider;

        public ILogger<BlockTransactionLimitConfigurationProcessor> Logger { get; set; }

        public BlockTransactionLimitConfigurationProcessor(IBlockTransactionLimitProvider blockTransactionLimitProvider)
        {
            _blockTransactionLimitProvider = blockTransactionLimitProvider;
        }


        public string ConfigurationName => "BlockTransactionLimit";
        
        public async Task ProcessConfigurationAsync(ByteString byteString, BlockIndex blockIndex)
        {
            var limit = new Int32Value();
            limit.MergeFrom(byteString);
            if (limit.Value < 0) return;
            await _blockTransactionLimitProvider.SetLimitAsync(blockIndex, limit.Value);
            Logger.LogDebug($"BlockTransactionLimit has been changed to {limit.Value}");
        }
    }
}