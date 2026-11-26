using System;
using System.Threading;
using _Project.Develop.Runtime.Infrastructure.Flow;
using _Project.Develop.Runtime.Infrastructure.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace _Project.Develop.Runtime.Infrastructure.SceneTransitions
{
    public sealed class GameTransitionService
    {
        private const float SCENE_LOAD_SHARE = 0.3f;

        private readonly ScenesService _scenesService;
        private bool _isRunning;

        public GameTransitionService(ScenesService scenesService)
        {
            _scenesService = scenesService;
        }

        public async UniTask SwitchToAsync(
            string sceneKey,
            IInputSceneArgs sceneArgs,
            IFlowProgress progress,
            CancellationToken cancellationToken)
        {
            if (_isRunning)
            {
                Debug.LogWarning($"Transition is already running, switch to {sceneKey} ignored");
                return;
            }

            _isRunning = true;
            ISceneBootstrap bootstrap;

            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                progress.SetStage($"Loading scene {sceneKey}");
                progress.Report(0f);

                Scene scene = await _scenesService.SwitchToAsync(
                    sceneKey, cancellationToken, sceneArgs, new RangeProgress(progress, 0f, SCENE_LOAD_SHARE));

                cancellationToken.ThrowIfCancellationRequested();
                bootstrap = ResolveBootstrap(scene, sceneKey);

                await bootstrap.InitializeAsync(new RangeProgress(progress, SCENE_LOAD_SHARE, 1f), cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
            }
            finally
            {
                _isRunning = false;
            }

            bootstrap.Run();
        }

        private static ISceneBootstrap ResolveBootstrap(Scene scene, string sceneKey)
        {
            LifetimeScope scope = LifetimeScope.Find<LifetimeScope>(scene);

            if (scope == null || scope.Container == null)
                throw new InvalidOperationException($"Scene '{sceneKey}' has no built LifetimeScope.");

            return scope.Container.Resolve<ISceneBootstrap>();
        }
    }
}
