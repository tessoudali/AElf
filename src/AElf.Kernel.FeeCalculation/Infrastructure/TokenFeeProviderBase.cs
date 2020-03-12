using System.Threading.Tasks;
using AElf.Contracts.MultiToken;
using AElf.Kernel.SmartContract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace AElf.Kernel.FeeCalculation.Infrastructure
{
    internal abstract class TokenFeeProviderBase
    {
        private readonly ICalculateFunctionProvider _calculateFunctionProvider;
        private readonly int _tokenType;
        public ILogger<TokenFeeProviderBase> Logger { get; set; }

        protected TokenFeeProviderBase(ICalculateFunctionProvider calculateFunctionProvider, int tokenType)
        {
            _tokenType = tokenType;
            _calculateFunctionProvider = calculateFunctionProvider;
            Logger = NullLogger<TokenFeeProviderBase>.Instance;
        }

        public Task<long> CalculateFeeAsync(ITransactionContext transactionContext, IChainContext chainContext)
        {
            var functionDictionary = _calculateFunctionProvider.GetCalculateFunctions(chainContext);
            var function = functionDictionary[((FeeTypeEnum) _tokenType).ToString().ToUpper()];
            var count = GetCalculateCount(transactionContext);
            return Task.FromResult(function.CalculateFee(count));
        }

        protected abstract int GetCalculateCount(ITransactionContext transactionContext);
    }
}