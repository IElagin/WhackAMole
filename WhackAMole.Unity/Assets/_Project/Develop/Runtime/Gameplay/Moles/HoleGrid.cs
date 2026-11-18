using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Moles
{
    public sealed class HoleGrid : MonoBehaviour
    {
        [SerializeField] private Transform[] _holes;

        public int Count => _holes.Length;

        public Transform GetHole(int index) => _holes[index];
    }
}
