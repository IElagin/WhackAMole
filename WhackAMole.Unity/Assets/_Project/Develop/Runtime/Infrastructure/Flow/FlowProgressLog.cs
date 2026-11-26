using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.Flow
{
    public sealed class FlowProgressLog : IFlowProgress
    {
        private const int PERCENT_MAX = 100;
        private const int PERCENT_LOG_STEP = 10;
        private const int NOTHING_LOGGED = -1;

        private int _lastLoggedStep = NOTHING_LOGGED;

        public void Report(float progress01)
        {
            int percent = Mathf.RoundToInt(Mathf.Clamp01(progress01) * PERCENT_MAX);
            int logStep = percent / PERCENT_LOG_STEP;

            if (logStep == _lastLoggedStep)
                return;

            _lastLoggedStep = logStep;
            Debug.Log($"[Progress] {percent}%");
        }

        public void SetStage(string text) => Debug.Log($"[Progress] {text}");
    }
}
