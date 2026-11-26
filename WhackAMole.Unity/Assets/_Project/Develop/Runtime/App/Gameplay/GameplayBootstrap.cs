using System.Threading;
using _Project.Develop.Runtime.Gameplay.Run;
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
        private readonly RunSession _runSession;

        public GameplayBootstrap(FlowRunner runner, GameplayStartupSteps startupSteps, RunSession runSession)
        {
            _runner = runner;
            _startupSteps = startupSteps;
            _runSession = runSession;
        }

        public UniTask InitializeAsync(IFlowProgress progress, CancellationToken cancellationToken) =>
            _runner.RunAsync(FLOW_NAME, _startupSteps.Build(), progress, cancellationToken);

        public void Run() => _runSession.Start();
    }
}
