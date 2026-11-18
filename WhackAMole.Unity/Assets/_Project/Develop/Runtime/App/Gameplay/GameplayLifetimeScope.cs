using _Project.Develop.Runtime.Gameplay;
using _Project.Develop.Runtime.Gameplay.Levels;
using _Project.Develop.Runtime.Gameplay.Moles;
using _Project.Develop.Runtime.Gameplay.Run;
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
            builder.RegisterComponent(_holeGrid);

            builder.Register<CurrentLevel>(Lifetime.Singleton);
            builder.Register<GameplayConsole>(Lifetime.Singleton);
            builder.Register<MoleFactory>(Lifetime.Singleton);
            builder.Register<MoleField>(Lifetime.Singleton);
            builder.Register<RunSession>(Lifetime.Singleton);
            builder.Register<RunFinisher>(Lifetime.Singleton);

            builder.RegisterEntryPoint<GameplayBootstrap>();
            builder.RegisterEntryPoint<MoleSpawner>();
            builder.RegisterEntryPoint<WhackInput>();
            builder.RegisterEntryPoint<RunEndInput>();
        }
    }
}
