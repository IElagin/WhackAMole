using System.Threading;
using _Project.Develop.Runtime.Infrastructure.Flow;
using _Project.Develop.Runtime.Infrastructure.SceneTransitions;
using Cysharp.Threading.Tasks;

namespace _Project.Develop.Runtime.App.Gameplay
{
    public sealed class GameplayBootstrap : ISceneBootstrap
    {
        private const string FLOW_NAME = "Gameplay startup";

        private readonly FlowRunner _runner;
        private readonly GameplayStartupSteps _startupSteps;
        private readonly GameplayRunLoop _runLoop;

        public GameplayBootstrap(FlowRunner runner, GameplayStartupSteps startupSteps, GameplayRunLoop runLoop)
        {
            _runner = runner;
            _startupSteps = startupSteps;
            _runLoop = runLoop;
        }

        public UniTask InitializeAsync(IFlowProgress progress, CancellationToken cancellationToken) =>
            _runner.RunAsync(FLOW_NAME, _startupSteps.Build(), progress, cancellationToken);

        public void Run() => _runLoop.Run();
    }
}
