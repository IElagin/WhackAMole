using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Levels
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig")]
    public sealed class LevelConfig : ScriptableObject
    {
        private const int MIN_GRID_SIZE = 1;
        private const int MAX_GRID_SIZE = 3;

        [SerializeField, Range(MIN_GRID_SIZE, MAX_GRID_SIZE)] private int _rows = MAX_GRID_SIZE;
        [SerializeField, Range(MIN_GRID_SIZE, MAX_GRID_SIZE)] private int _columns = MAX_GRID_SIZE;
        [SerializeField] private int _requiredHits;
        [SerializeField] private int _lives;
        [SerializeField] private float _spawnInterval;
        [SerializeField] private float _visibleDuration;
        [SerializeField] private int _goldPerHit;

        public int Rows => Mathf.Clamp(_rows, MIN_GRID_SIZE, MAX_GRID_SIZE);

        public int Columns => Mathf.Clamp(_columns, MIN_GRID_SIZE, MAX_GRID_SIZE);

        public int RequiredHits => _requiredHits;

        public int Lives => _lives;

        public float SpawnInterval => _spawnInterval;

        public float VisibleDuration => _visibleDuration;

        public int GoldPerHit => _goldPerHit;
    }
}
