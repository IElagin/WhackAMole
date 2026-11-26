using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace _Project.Develop.Runtime.Infrastructure.SceneManagement
{
    public sealed class ScenesService
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly string _bufferSceneKey;

        public ScenesService(ISceneLoader sceneLoader, string bufferSceneKey)
        {
            _sceneLoader = sceneLoader;
            _bufferSceneKey = bufferSceneKey;
        }

        public async UniTask<Scene> SwitchToAsync(
            string sceneKey,
            CancellationToken cancellationToken,
            IInputSceneArgs sceneArgs = null,
            IProgress<float> progress = null)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (Application.CanStreamedLevelBeLoaded(sceneKey) == false)
                throw new InvalidOperationException($"Scene '{sceneKey}' is not available in the build.");

            Scene buffer = SceneManager.GetSceneByName(_bufferSceneKey);

            if (buffer.isLoaded == false)
                buffer = await _sceneLoader.LoadAsync(_bufferSceneKey, cancellationToken);

            await ActivateAndUnloadOthersAsync(buffer, cancellationToken);

            Action<IContainerBuilder> extraRegistrations = null;

            if (sceneArgs != null)
                extraRegistrations = builder => builder.RegisterInstance(sceneArgs, sceneArgs.GetType());

            Scene target = await _sceneLoader.LoadAsync(sceneKey, cancellationToken, extraRegistrations, progress);
            await ActivateAndUnloadOthersAsync(target, cancellationToken);

            return target;
        }

        private async UniTask ActivateAndUnloadOthersAsync(Scene activeScene, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (activeScene.isLoaded == false || SceneManager.SetActiveScene(activeScene) == false)
                throw new InvalidOperationException($"Failed to activate scene '{activeScene.name}'.");

            for (int i = SceneManager.sceneCount - 1; i >= 0; i--)
            {
                cancellationToken.ThrowIfCancellationRequested();
                Scene scene = SceneManager.GetSceneAt(i);

                if (scene != activeScene && scene.isLoaded)
                    await _sceneLoader.UnloadAsync(scene, cancellationToken);
            }
        }
    }
}
