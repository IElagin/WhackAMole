using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.ConfigsService.ConfigLoader
{
    public sealed class ResourcesConfigLoader : IConfigLoader
    {
        private readonly IReadOnlyDictionary<Type, string> _configPaths;

        public ResourcesConfigLoader(IReadOnlyDictionary<Type, string> configPaths)
        {
            _configPaths = configPaths;
        }

        public UniTask<int> CountConfigsAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return UniTask.FromResult(_configPaths.Count);
        }

        public async UniTask LoadAsync(Action<Type, object> addConfig, CancellationToken cancellationToken)
        {
            List<UniTask> tasks = new(_configPaths.Count);

            foreach (KeyValuePair<Type, string> configPath in _configPaths)
            {
                tasks.Add(LoadConfigAsync(configPath.Key, configPath.Value, addConfig, cancellationToken));
            }

            await UniTask.WhenAll(tasks);
        }

        private static async UniTask LoadConfigAsync(
            Type type,
            string path,
            Action<Type, object> addConfig,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            UnityEngine.Object config = await Resources
                .LoadAsync(path, type)
                .ToUniTask(cancellationToken: cancellationToken);

            if (config == null)
                throw new InvalidOperationException(
                    $"Config of type '{type.Name}' is not found in Resources at path '{path}'.");

            addConfig(type, config);
        }
    }
}
