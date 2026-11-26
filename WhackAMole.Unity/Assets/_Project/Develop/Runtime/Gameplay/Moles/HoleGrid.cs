using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Moles
{
    public sealed class HoleGrid : MonoBehaviour
    {
        private const float HOLE_SPACING = 2.2f;

        [SerializeField] private Transform _holePrefab;

        public Transform CreateHole(int index, int rows, int columns)
        {
            int column = index % columns;
            int row = index / columns;
            float x = (column - (columns - 1) * 0.5f) * HOLE_SPACING;
            float z = ((rows - 1) * 0.5f - row) * HOLE_SPACING;

            Transform hole = Instantiate(_holePrefab, transform);
            hole.name = $"Hole{index + 1}";
            hole.localPosition = new Vector3(x, 0f, z);

            return hole;
        }
    }
}
