using _Project.Develop.Runtime.Gameplay;
using _Project.Develop.Runtime.Infrastructure.ConfigsService;
using _Project.Develop.Runtime.Infrastructure.Flow;
using _Project.Develop.Runtime.Infrastructure.SceneManagement;
using _Project.Develop.Runtime.Infrastructure.SceneTransitions;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Develop.Runtime.App.Navigation
{
    public sealed class GameNavigation
    {
        private readonly GameTransitionService _transitionService;
        private readonly ConfigsProviderService _configsProviderService;
        private readonly FlowProgressLog _progressLog;

        private GameNavigationConfig _config;

        public GameNavigation(
            GameTransitionService transitionService,
            ConfigsProviderService configsProviderService,
            FlowProgressLog progressLog)
        {
            _transitionService = transitionService;
            _configsProviderService = configsProviderService;
            _progressLog = progressLog;
        }

        private GameNavigationConfig Config
        {
            get
            {
                if (_config == null)
                    _config = _configsProviderService.GetConfig<GameNavigationConfig>();

                return _config;
            }
        }

        public void OpenMainMenu() => SwitchTo(Config.MainMenuSceneKey, null);

        public void OpenGameplay(int levelNumber) =>
            SwitchTo(Config.GameplaySceneKey, new GameplayArgs(levelNumber));

        private void SwitchTo(string sceneKey, IInputSceneArgs sceneArgs)
        {
            // The transition must survive unloading the scene that requested it.
            _transitionService
                .SwitchToAsync(sceneKey, sceneArgs, _progressLog, Application.exitCancellationToken)
                .Forget();
        }
    }
}
