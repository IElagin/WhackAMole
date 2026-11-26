using System;
using System.Collections.Generic;
using System.Threading;
using _Project.Develop.Runtime.Infrastructure.ConfigsService.ConfigLoader;
using Cysharp.Threading.Tasks;

namespace _Project.Develop.Runtime.Infrastructure.ConfigsService
{
    public sealed class ConfigsProviderService
    {
        private readonly IEnumerable<IConfigLoader> _loaders;
        private Dictionary<Type, object> _configs = new();

        public ConfigsProviderService(IEnumerable<IConfigLoader> loaders)
        {
            _loaders = loaders;
        }

        public async UniTask LoadAsync(
            IProgress<ConfigsLoadProgress> progress,
            CancellationToken cancellationToken)
        {
            int total = 0;

            foreach (IConfigLoader loader in _loaders)
                total += await loader.CountConfigsAsync(cancellationToken);

            Dictionary<Type, object> configs = new(total);
            progress?.Report(new ConfigsLoadProgress(0, total));

            List<UniTask> tasks = new();

            foreach (IConfigLoader loader in _loaders)
                tasks.Add(loader.LoadAsync(AddConfig, cancellationToken));

            await UniTask.WhenAll(tasks);
            cancellationToken.ThrowIfCancellationRequested();

            _configs = configs;

            void AddConfig(Type type, object config)
            {
                if (configs.TryAdd(type, config) == false)
                    throw new InvalidOperationException(
                        $"Duplicate config type '{type.Name}': more than one source provides a config of this type.");

                progress?.Report(new ConfigsLoadProgress(configs.Count, total));
            }
        }

        public T GetConfig<T>() where T : class
        {
            if (_configs.TryGetValue(typeof(T), out object config) == false)
                throw new InvalidOperationException(
                    $"Config of type '{typeof(T).Name}' is not loaded. " +
                    "Ensure LoadAsync has completed and one of the loaders provides this config.");

            return (T)config;
        }

    }
}
