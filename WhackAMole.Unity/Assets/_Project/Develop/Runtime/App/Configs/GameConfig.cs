using UnityEngine;

namespace _Project.Develop.Runtime.App.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public sealed class GameConfig : ScriptableObject
    {
        [SerializeField] private int _startingGold;

        public int StartingGold => _startingGold;
    }
}
