using System.Threading;
using _Project.Develop.Runtime.Infrastructure.Flow;
using Cysharp.Threading.Tasks;

namespace _Project.Develop.Runtime.App.MainMenu.StartupSteps
{
    public sealed class PrepareMainMenuStep : IFlowStep
    {
        private readonly MainMenuScreen _screen;

        public PrepareMainMenuStep(MainMenuScreen screen)
        {
            _screen = screen;
        }

        public string Name => "Prepare main menu";

        public UniTask ExecuteAsync(IFlowProgress progress, CancellationToken cancellationToken)
        {
            _screen.PrepareStatus();
            return UniTask.CompletedTask;
        }
    }
}
