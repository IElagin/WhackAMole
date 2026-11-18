using _Project.Develop.Runtime.App.Configs;
using _Project.Develop.Runtime.App.Navigation;
using _Project.Develop.Runtime.Infrastructure.ConfigsService;
using _Project.Develop.Runtime.Meta.Features.Economy;
using UnityEngine;
using VContainer.Unity;

namespace _Project.Develop.Runtime.App.Bootstrap
{
    public sealed class AppBootstrap : IStartable
    {
        private const int TARGET_FRAME_RATE = 60;

        private readonly ConfigsProviderService _configsProviderService;
        private readonly WalletService _wallet;
        private readonly GameNavigation _gameNavigation;

        public AppBootstrap(
            ConfigsProviderService configsProviderService,
            WalletService wallet,
            GameNavigation gameNavigation)
        {
            _configsProviderService = configsProviderService;
            _wallet = wallet;
            _gameNavigation = gameNavigation;
        }

        public void Start()
        {
            SetupApp();

            _configsProviderService.Load();

            GrantStartingGold();

            _gameNavigation.OpenMainMenu();
        }

        private void SetupApp()
        {
            Application.targetFrameRate = TARGET_FRAME_RATE;
            QualitySettings.vSyncCount = 0;
        }

        private void GrantStartingGold()
        {
            int startingGold = _configsProviderService.GetConfig<GameConfig>().StartingGold;

            if (startingGold > 0)
                _wallet.Add(CurrencyId.Gold, startingGold);
        }
    }
}
