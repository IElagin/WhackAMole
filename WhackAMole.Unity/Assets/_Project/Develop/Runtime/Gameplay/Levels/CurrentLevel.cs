using _Project.Develop.Runtime.Infrastructure.ConfigsService;

namespace _Project.Develop.Runtime.Gameplay.Levels
{
    public sealed class CurrentLevel
    {
        private readonly ConfigsProviderService _configsProviderService;

        private LevelConfig _config;

        public CurrentLevel(GameplayArgs args, ConfigsProviderService configsProviderService)
        {
            Number = args.LevelNumber;
            _configsProviderService = configsProviderService;
        }

        public int Number { get; }

        public LevelConfig Config
        {
            get
            {
                if (_config == null)
                    _config = _configsProviderService.GetConfig<LevelsConfig>().GetLevel(Number);

                return _config;
            }
        }
    }
}
