using System.Collections.Generic;
using _Project.Develop.Runtime.App.Bootstrap.StartupSteps;
using _Project.Develop.Runtime.Infrastructure.Flow;

namespace _Project.Develop.Runtime.App.Bootstrap
{
    public sealed class AppStartupSteps
    {
        private readonly FlowStepsFactory _factory;

        public AppStartupSteps(FlowStepsFactory factory)
        {
            _factory = factory;
        }

        public IReadOnlyList<FlowStep> Build() => new[]
        {
            new FlowStep(_factory.Create<LoadConfigsStep>(), 1f),
            new FlowStep(_factory.Create<GrantStartingGoldStep>()),
        };
    }
}
