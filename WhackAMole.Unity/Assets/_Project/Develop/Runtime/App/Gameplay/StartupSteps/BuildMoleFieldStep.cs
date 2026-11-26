using System.Threading;
using _Project.Develop.Runtime.Gameplay.Levels;
using _Project.Develop.Runtime.Gameplay.Moles;
using _Project.Develop.Runtime.Infrastructure.Flow;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Develop.Runtime.App.Gameplay.StartupSteps
{
    public sealed class BuildMoleFieldStep : IFlowStep
    {
        private readonly CurrentLevel _currentLevel;
        private readonly MoleField _moleField;

        public BuildMoleFieldStep(CurrentLevel currentLevel, MoleField moleField)
        {
            _currentLevel = currentLevel;
            _moleField = moleField;
        }

        public string Name => "Build mole field";

        public async UniTask ExecuteAsync(IFlowProgress progress, CancellationToken cancellationToken)
        {
            LevelConfig config = _currentLevel.Config;
            int rows = config.Rows;
            int columns = config.Columns;
            int holeCount = rows * columns;

            for (int readyHoles = 1; readyHoles <= holeCount; readyHoles++)
            {
                await UniTask.NextFrame(cancellationToken: cancellationToken);
                _moleField.AddHole(rows, columns);

                float completion = (float)readyHoles / holeCount;
                progress.SetStage($"Готово лунок {readyHoles}/{holeCount} ({Mathf.RoundToInt(completion * 100f)}%)");
                progress.Report(completion);
            }
        }
    }
}
