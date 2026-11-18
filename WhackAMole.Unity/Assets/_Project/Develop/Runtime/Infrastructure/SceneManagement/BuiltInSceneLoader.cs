using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace _Project.Develop.Runtime.Infrastructure.SceneManagement
{
    public sealed class BuiltInSceneLoader : ISceneLoader
    {
        private bool _isLoading;

        public void Load(string sceneKey)
        {
            if (_isLoading)
            {
                Debug.LogWarning($"Scene '{sceneKey}' is not loaded: another scene load is in progress.");
                return;
            }

            StartLoading(sceneKey, null);
        }

        public void Load<TArgs>(string sceneKey, TArgs args)
        {
            if (_isLoading)
            {
                Debug.LogWarning($"Scene '{sceneKey}' is not loaded: another scene load is in progress.");
                return;
            }

            IDisposable extraRegistrations = LifetimeScope.Enqueue(builder => builder.RegisterInstance(args));

            StartLoading(sceneKey, extraRegistrations);
        }

        private void StartLoading(string sceneKey, IDisposable extraRegistrations)
        {
            _isLoading = true;

            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneKey);

            if (operation == null)
            {
                extraRegistrations?.Dispose();
                _isLoading = false;

                throw new InvalidOperationException($"Failed to start loading scene '{sceneKey}'. Ensure the scene is added to Build Profiles.");
            }

            operation.completed += _ =>
            {
                extraRegistrations?.Dispose();
                _isLoading = false;
            };
        }
    }
}
