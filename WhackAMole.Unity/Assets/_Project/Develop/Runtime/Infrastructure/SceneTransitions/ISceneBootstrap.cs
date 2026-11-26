using System.Threading;
using _Project.Develop.Runtime.Infrastructure.Flow;
using Cysharp.Threading.Tasks;

namespace _Project.Develop.Runtime.Infrastructure.SceneTransitions
{
    public interface ISceneBootstrap
    {
        UniTask InitializeAsync(IFlowProgress progress, CancellationToken cancellationToken);

        void Run();
    }
}
