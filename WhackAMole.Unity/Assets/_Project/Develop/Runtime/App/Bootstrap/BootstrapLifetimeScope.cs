using _Project.Develop.Runtime.App.Bootstrap.StartupSteps;
using _Project.Develop.Runtime.Infrastructure.Flow;
using VContainer;
using VContainer.Unity;

namespace _Project.Develop.Runtime.App.Bootstrap
{
    public class BootstrapLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<AppBootstrap>();
            builder.Register<AppStartupSteps>(Lifetime.Singleton);
            builder.Register<FlowStepsFactory>(Lifetime.Singleton);

            builder.Register<LoadConfigsStep>(Lifetime.Transient);
            builder.Register<GrantStartingGoldStep>(Lifetime.Transient);
        }
    }
}
