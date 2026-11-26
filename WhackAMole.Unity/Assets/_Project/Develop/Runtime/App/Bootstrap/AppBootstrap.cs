using System.Threading;
using _Project.Develop.Runtime.App.Navigation;
using _Project.Develop.Runtime.Infrastructure.Flow;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace _Project.Develop.Runtime.App.Bootstrap
{
    public sealed class AppBootstrap : IAsyncStartable
    {
        private const string FLOW_NAME = "App startup";
        private const int TARGET_FRAME_RATE = 60;

        private readonly FlowRunner _runner;
        private readonly AppStartupSteps _startupSteps;
        private readonly GameNavigation _gameNavigation;
        private readonly FlowProgressLog _progressLog;

        public AppBootstrap(
            FlowRunner runner,
            AppStartupSteps startupSteps,
            GameNavigation gameNavigation,
            FlowProgressLog progressLog)
        {
            _runner = runner;
            _startupSteps = startupSteps;
            _gameNavigation = gameNavigation;
            _progressLog = progressLog;
        }

        public async UniTask StartAsync(CancellationToken cancellationToken)
        {
            SetupApp();

            await _runner.RunAsync(FLOW_NAME, _startupSteps.Build(), _progressLog, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();

            _gameNavigation.OpenMainMenu();
        }

        private static void SetupApp()
        {
            Application.targetFrameRate = TARGET_FRAME_RATE;
            QualitySettings.vSyncCount = 0;
        }

    }
}
