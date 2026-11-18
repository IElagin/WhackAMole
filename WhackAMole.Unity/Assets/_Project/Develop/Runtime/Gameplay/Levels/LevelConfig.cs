using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Levels
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig")]
    public sealed class LevelConfig : ScriptableObject
    {
        [SerializeField] private int _requiredHits;
        [SerializeField] private int _lives;
        [SerializeField] private float _spawnInterval;
        [SerializeField] private float _visibleDuration;
        [SerializeField] private int _goldPerHit;

        public int RequiredHits => _requiredHits;

        public int Lives => _lives;

        public float SpawnInterval => _spawnInterval;

        public float VisibleDuration => _visibleDuration;

        public int GoldPerHit => _goldPerHit;
    }
}
