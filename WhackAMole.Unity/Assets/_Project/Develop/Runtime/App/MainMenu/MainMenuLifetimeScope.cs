using _Project.Develop.Runtime.App.MainMenu.StartupSteps;
using _Project.Develop.Runtime.Infrastructure.Flow;
using _Project.Develop.Runtime.Infrastructure.SceneTransitions;
using VContainer;
using VContainer.Unity;

namespace _Project.Develop.Runtime.App.MainMenu
{
    public class MainMenuLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<LevelSelector>(Lifetime.Singleton);
            builder.Register<MainMenuScreen>(Lifetime.Singleton);

            builder.Register<ISceneBootstrap, MainMenuBootstrap>(Lifetime.Singleton);
            builder.Register<MainMenuStartupSteps>(Lifetime.Singleton);
            builder.Register<FlowStepsFactory>(Lifetime.Singleton);
            builder.Register<PrepareMainMenuStep>(Lifetime.Transient);

            builder.RegisterEntryPoint<MainMenuInput>().AsSelf();
        }
    }
}
