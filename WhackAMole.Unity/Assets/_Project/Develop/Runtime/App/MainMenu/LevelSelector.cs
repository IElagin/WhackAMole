using _Project.Develop.Runtime.App.Navigation;
using _Project.Develop.Runtime.Gameplay.Levels;
using _Project.Develop.Runtime.Infrastructure.ConfigsService;
using _Project.Develop.Runtime.Meta.Features.Progress;

namespace _Project.Develop.Runtime.App.MainMenu
{
    public sealed class LevelSelector
    {
        private readonly ConfigsProviderService _configsProviderService;
        private readonly PlayerProgressService _playerProgress;
        private readonly GameNavigation _gameNavigation;

        public LevelSelector(
            ConfigsProviderService configsProviderService,
            PlayerProgressService playerProgress,
            GameNavigation gameNavigation)
        {
            _configsProviderService = configsProviderService;
            _playerProgress = playerProgress;
            _gameNavigation = gameNavigation;
        }

        public int LevelCount => _configsProviderService.GetConfig<LevelsConfig>().Count;

        public bool IsOpen(int levelNumber) => levelNumber <= _playerProgress.CurrentLevel;

        public LevelSelectResult Select(int levelNumber)
        {
            if (levelNumber < 1 || levelNumber > LevelCount)
                return LevelSelectResult.NotFound;

            if (IsOpen(levelNumber) == false)
                return LevelSelectResult.Locked;

            _gameNavigation.OpenGameplay(levelNumber);

            return LevelSelectResult.Started;
        }
    }
}
