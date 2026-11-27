using _Project.Develop.Runtime.App.Gameplay.StartupSteps;
using _Project.Develop.Runtime.Gameplay;
using _Project.Develop.Runtime.Gameplay.Levels;
using _Project.Develop.Runtime.Gameplay.Moles;
using _Project.Develop.Runtime.Gameplay.Run;
using _Project.Develop.Runtime.Infrastructure.Flow;
using _Project.Develop.Runtime.Infrastructure.SceneTransitions;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Develop.Runtime.App.Gameplay
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        [SerializeField] private HoleGrid _holeGrid;

        protected override void Configure(IContainerBuilder builder)
        {
            InstallStartupSteps(builder);

            builder.RegisterComponent(_holeGrid);

            builder.Register<CurrentLevel>(Lifetime.Singleton);
            builder.Register<GameplayConsole>(Lifetime.Singleton);
            builder.Register<MoleFactory>(Lifetime.Singleton);
            builder.Register<MoleField>(Lifetime.Singleton);
            builder.Register<RunSession>(Lifetime.Singleton);
            builder.Register<RunFinisher>(Lifetime.Singleton);

            builder.Register<MoleSpawner>(Lifetime.Singleton);
            builder.Register<WhackInput>(Lifetime.Singleton);
            builder.Register<RunEndInput>(Lifetime.Singleton);
            builder.Register<RunStatesFactory>(Lifetime.Singleton);
            builder.RegisterEntryPoint<GameplayRunLoop>().AsSelf();
        }

        private static void InstallStartupSteps(IContainerBuilder builder)
        {
            builder.Register<ISceneBootstrap, GameplayBootstrap>(Lifetime.Singleton);
            builder.Register<GameplayStartupSteps>(Lifetime.Singleton);
            builder.Register<FlowStepsFactory>(Lifetime.Singleton);

            builder.Register<LoadMolePrefabStep>(Lifetime.Transient);
            builder.Register<BuildMoleFieldStep>(Lifetime.Transient);
        }
    }
}
