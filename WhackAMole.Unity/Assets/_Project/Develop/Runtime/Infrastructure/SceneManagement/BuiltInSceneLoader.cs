using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace _Project.Develop.Runtime.Infrastructure.SceneManagement
{
    public sealed class BuiltInSceneLoader : ISceneLoader, IDisposable
    {
        private IDisposable _registrationsWindow;

        public async UniTask<Scene> LoadAsync(
            string sceneKey,
            CancellationToken cancellationToken,
            Action<IContainerBuilder> extraRegistrations = null,
            IProgress<float> progress = null)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // The target scope receives these registrations during Awake.
            if (extraRegistrations != null)
                _registrationsWindow = LifetimeScope.Enqueue(extraRegistrations);

            try
            {
                AsyncOperation operation = SceneManager.LoadSceneAsync(sceneKey, LoadSceneMode.Additive);

                if (operation == null)
                    throw new InvalidOperationException($"Failed to start loading scene '{sceneKey}'.");

                await operation.ToUniTask(progress, cancellationToken: cancellationToken);
                return SceneManager.GetSceneByName(sceneKey);
            }
            finally
            {
                CloseRegistrationsWindow();
            }
        }

        public UniTask UnloadAsync(Scene scene, CancellationToken cancellationToken) =>
            SceneManager.UnloadSceneAsync(scene).ToUniTask(cancellationToken: cancellationToken);

        // The root can be disposed while an async load is still pending.
        public void Dispose() => CloseRegistrationsWindow();

        private void CloseRegistrationsWindow()
        {
            _registrationsWindow?.Dispose();
            _registrationsWindow = null;
        }
    }
}
