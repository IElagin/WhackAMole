using System;

namespace _Project.Develop.Runtime.Infrastructure.Flow
{
    public interface IFlowProgress : IProgress<float>
    {
        void SetStage(string text);
    }
}
