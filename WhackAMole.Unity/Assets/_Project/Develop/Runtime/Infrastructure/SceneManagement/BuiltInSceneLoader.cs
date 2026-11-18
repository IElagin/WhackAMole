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
                Debug.LogWarning($"Сцена '{sceneKey}' не загружена: другая загрузка ещё идёт");
                return;
            }

            StartLoading(sceneKey, null);
        }

        public void Load<TArgs>(string sceneKey, TArgs args)
        {
            if (_isLoading)
            {
                Debug.LogWarning($"Сцена '{sceneKey}' не загружена: другая загрузка ещё идёт");
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

                throw new InvalidOperationException($"Не удалось начать загрузку сцены '{sceneKey}': проверьте имя и Build Profiles");
            }

            operation.completed += _ =>
            {
                extraRegistrations?.Dispose();
                _isLoading = false;
            };
        }
    }
}
