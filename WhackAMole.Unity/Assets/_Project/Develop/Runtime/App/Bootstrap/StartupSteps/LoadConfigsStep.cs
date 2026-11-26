using System;
using System.Threading;
using _Project.Develop.Runtime.Infrastructure.ConfigsService;
using _Project.Develop.Runtime.Infrastructure.Flow;
using Cysharp.Threading.Tasks;

namespace _Project.Develop.Runtime.App.Bootstrap.StartupSteps
{
    public sealed class LoadConfigsStep : IFlowStep, IProgress<ConfigsLoadProgress>
    {
        private const string CONFIGS_STAGE_TEXT = "Loading configs";

        private readonly ConfigsProviderService _configsProviderService;

        private IFlowProgress _progress;

        public LoadConfigsStep(ConfigsProviderService configsProviderService)
        {
            _configsProviderService = configsProviderService;
        }

        public string Name => "Load configs";

        public UniTask ExecuteAsync(IFlowProgress progress, CancellationToken cancellationToken)
        {
            _progress = progress;
            progress.SetStage(CONFIGS_STAGE_TEXT);

            return _configsProviderService.LoadAsync(this, cancellationToken);
        }

        public void Report(ConfigsLoadProgress value)
        {
            _progress.SetStage($"{CONFIGS_STAGE_TEXT} {value.Loaded}/{value.Total}");
            _progress.Report(value.Percent);
        }
    }
}
