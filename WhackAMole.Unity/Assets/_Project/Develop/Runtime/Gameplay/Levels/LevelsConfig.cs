using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Levels
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "Configs/LevelsConfig")]
    public sealed class LevelsConfig : ScriptableObject
    {
        private const int MAX_LEVELS = 9;

        [SerializeField] private List<LevelConfig> _levels;

        public int Count => _levels.Count;

        public LevelConfig GetLevel(int levelNumber)
        {
            if (levelNumber < 1 || levelNumber > _levels.Count)
                throw new ArgumentOutOfRangeException(nameof(levelNumber), $"Уровня {levelNumber} нет: всего {_levels.Count}");

            return _levels[levelNumber - 1];
        }

        private void OnValidate()
        {
            if (_levels != null && _levels.Count > MAX_LEVELS)
                Debug.LogWarning($"Уровней больше {MAX_LEVELS}: лишние нельзя выбрать клавишами 1–9", this);
        }
    }
}
