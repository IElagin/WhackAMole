using System.Threading;
using Cysharp.Threading.Tasks;

namespace _Project.Develop.Runtime.Infrastructure.Flow
{
    public interface IFlowStep
    {
        string Name { get; }

        UniTask ExecuteAsync(IFlowProgress progress, CancellationToken cancellationToken);
    }
}
