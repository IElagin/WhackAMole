using System.Collections.Generic;
using _Project.Develop.Runtime.App.Gameplay.StartupSteps;
using _Project.Develop.Runtime.Infrastructure.Flow;

namespace _Project.Develop.Runtime.App.Gameplay
{
    public sealed class GameplayStartupSteps
    {
        private readonly FlowStepsFactory _factory;

        public GameplayStartupSteps(FlowStepsFactory factory)
        {
            _factory = factory;
        }

        public IReadOnlyList<FlowStep> Build() => new[]
        {
            new FlowStep(_factory.Create<LoadMolePrefabStep>(), 1f),
            new FlowStep(_factory.Create<BuildMoleFieldStep>(), 1f),
        };
    }
}
