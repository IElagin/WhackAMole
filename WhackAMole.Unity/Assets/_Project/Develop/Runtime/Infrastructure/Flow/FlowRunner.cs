using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.Flow
{
    public sealed class FlowRunner
    {
        public async UniTask RunAsync(
            string flowName,
            IReadOnlyList<FlowStep> steps,
            IFlowProgress progress,
            CancellationToken cancellationToken)
        {
            int stepNumber = 0;
            string stepName = string.Empty;

            float totalWeight = SumWeights(steps);
            float passedWeight = 0f;

            try
            {
                progress.Report(0f);

                for (int i = 0; i < steps.Count; i++)
                {
                    // Синхронный шаг токен не смотрит — отмену между шагами проверяет раннер
                    cancellationToken.ThrowIfCancellationRequested();

                    FlowStep flowStep = steps[i];
                    stepNumber = i + 1;
                    stepName = flowStep.Step.Name;

                    float from = Share(passedWeight, totalWeight);
                    passedWeight += flowStep.Weight;
                    float to = Share(passedWeight, totalWeight);

                    Debug.Log($"[Flow] {flowName}: {stepNumber}/{steps.Count} '{stepName}'");
                    await flowStep.Step.ExecuteAsync(new RangeProgress(progress, from, to), cancellationToken);
                }

                cancellationToken.ThrowIfCancellationRequested();
                progress.Report(1f);
            }
            catch (OperationCanceledException)
            {
                Debug.LogWarning($"[Flow] {flowName} canceled at step {stepNumber}/{steps.Count} '{stepName}'");
                throw;
            }
            catch (Exception)
            {
                Debug.LogError($"[Flow] {flowName} failed at step {stepNumber}/{steps.Count} '{stepName}'");
                throw;
            }
        }

        // Список без весомых шагов держит полоску на нуле и закрывает её финальным репортом
        private static float Share(float weight, float totalWeight) =>
            totalWeight > 0f ? weight / totalWeight : 0f;

        private static float SumWeights(IReadOnlyList<FlowStep> steps)
        {
            float total = 0f;

            for (int i = 0; i < steps.Count; i++)
                total += steps[i].Weight;

            return total;
        }
    }
}
