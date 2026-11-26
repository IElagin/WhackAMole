using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace _Project.Develop.Runtime.Infrastructure.ConfigsService.ConfigLoader
{
    public interface IConfigLoader
    {
        UniTask<int> CountConfigsAsync(CancellationToken cancellationToken);

        UniTask LoadAsync(Action<Type, object> addConfig, CancellationToken cancellationToken);
    }
}
