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

            builder.RegisterEntryPoint<MainMenuBootstrap>();
            builder.RegisterEntryPoint<MainMenuInput>();
        }
    }
}
