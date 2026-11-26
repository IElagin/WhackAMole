using VContainer;

namespace _Project.Develop.Runtime.Infrastructure.Flow
{
    public sealed class FlowStepsFactory
    {
        private readonly IObjectResolver _resolver;

        public FlowStepsFactory(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public TStep Create<TStep>() where TStep : IFlowStep => _resolver.Resolve<TStep>();
    }
}
