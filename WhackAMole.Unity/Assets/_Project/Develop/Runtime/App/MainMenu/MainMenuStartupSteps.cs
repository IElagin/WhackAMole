using System.Collections.Generic;
using _Project.Develop.Runtime.App.MainMenu.StartupSteps;
using _Project.Develop.Runtime.Infrastructure.Flow;

namespace _Project.Develop.Runtime.App.MainMenu
{
    public sealed class MainMenuStartupSteps
    {
        private readonly FlowStepsFactory _factory;

        public MainMenuStartupSteps(FlowStepsFactory factory)
        {
            _factory = factory;
        }

        public IReadOnlyList<FlowStep> Build() => new[]
        {
            new FlowStep(_factory.Create<PrepareMainMenuStep>()),
        };
    }
}
