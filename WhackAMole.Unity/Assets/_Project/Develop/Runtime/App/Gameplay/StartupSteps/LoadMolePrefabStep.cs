using System.Threading;
using _Project.Develop.Runtime.Gameplay.Moles;
using _Project.Develop.Runtime.Infrastructure.Flow;
using Cysharp.Threading.Tasks;

namespace _Project.Develop.Runtime.App.Gameplay.StartupSteps
{
    public sealed class LoadMolePrefabStep : IFlowStep
    {
        private readonly MoleFactory _moleFactory;

        public LoadMolePrefabStep(MoleFactory moleFactory)
        {
            _moleFactory = moleFactory;
        }

        public string Name => "Load mole prefab";

        public UniTask ExecuteAsync(IFlowProgress progress, CancellationToken cancellationToken) =>
            _moleFactory.LoadPrefabAsync(progress, cancellationToken);
    }
}
