namespace _Project.Develop.Runtime.Infrastructure.Flow
{
    // Вес есть у шага, который заметно ждёт диск или сеть; без веса шаг полоску не двигает
    public readonly struct FlowStep
    {
        public const float DEFAULT_WEIGHT = 0f;

        public readonly IFlowStep Step;
        public readonly float Weight;

        public FlowStep(IFlowStep step, float weight = DEFAULT_WEIGHT)
        {
            Step = step;
            Weight = weight;
        }
    }
}
