using System.Threading;
using _Project.Develop.Runtime.Infrastructure.Flow;
using _Project.Develop.Runtime.Infrastructure.SceneTransitions;
using Cysharp.Threading.Tasks;

namespace _Project.Develop.Runtime.App.MainMenu
{
    public sealed class MainMenuBootstrap : ISceneBootstrap
    {
        private const string FLOW_NAME = "Main menu startup";

        private readonly FlowRunner _runner;
        private readonly MainMenuStartupSteps _startupSteps;
        private readonly MainMenuScreen _screen;
        private readonly MainMenuInput _input;

        public MainMenuBootstrap(
            FlowRunner runner,
            MainMenuStartupSteps startupSteps,
            MainMenuScreen screen,
            MainMenuInput input)
        {
            _runner = runner;
            _startupSteps = startupSteps;
            _screen = screen;
            _input = input;
        }

        public UniTask InitializeAsync(IFlowProgress progress, CancellationToken cancellationToken) =>
            _runner.RunAsync(FLOW_NAME, _startupSteps.Build(), progress, cancellationToken);

        public void Run()
        {
            _screen.ShowStatus();
            _input.Enable();
        }
    }
}
