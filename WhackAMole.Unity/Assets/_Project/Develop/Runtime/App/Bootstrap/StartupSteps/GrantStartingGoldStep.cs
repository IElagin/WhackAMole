using System.Threading;
using _Project.Develop.Runtime.App.Configs;
using _Project.Develop.Runtime.Infrastructure.ConfigsService;
using _Project.Develop.Runtime.Infrastructure.Flow;
using _Project.Develop.Runtime.Meta.Features.Economy;
using Cysharp.Threading.Tasks;

namespace _Project.Develop.Runtime.App.Bootstrap.StartupSteps
{
    public sealed class GrantStartingGoldStep : IFlowStep
    {
        private readonly ConfigsProviderService _configsProviderService;
        private readonly WalletService _wallet;

        public GrantStartingGoldStep(ConfigsProviderService configsProviderService, WalletService wallet)
        {
            _configsProviderService = configsProviderService;
            _wallet = wallet;
        }

        public string Name => "Grant starting gold";

        public UniTask ExecuteAsync(IFlowProgress progress, CancellationToken cancellationToken)
        {
            int startingGold = _configsProviderService.GetConfig<GameConfig>().StartingGold;

            if (startingGold > 0)
                _wallet.Add(CurrencyId.Gold, startingGold);

            return UniTask.CompletedTask;
        }
    }
}
