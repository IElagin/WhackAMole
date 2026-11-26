using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.Flow
{
    public sealed class RangeProgress : IFlowProgress
    {
        private readonly IFlowProgress _parent;
        private readonly float _from;
        private readonly float _to;

        public RangeProgress(IFlowProgress parent, float from, float to)
        {
            _parent = parent;
            _from = from;
            _to = to;
        }

        public void Report(float progress01) =>
            _parent.Report(Mathf.Lerp(_from, _to, Mathf.Clamp01(progress01)));

        public void SetStage(string text) => _parent.SetStage(text);
    }
}
