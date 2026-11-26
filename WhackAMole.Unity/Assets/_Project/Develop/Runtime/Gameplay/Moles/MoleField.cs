using System.Collections.Generic;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Moles
{
    public sealed class MoleField
    {
        private readonly HoleGrid _holeGrid;
        private readonly MoleFactory _moleFactory;

        private readonly List<Mole> _moles = new();
        private readonly List<float> _timers = new();
        private readonly List<int> _freeIndices = new();

        public MoleField(HoleGrid holeGrid, MoleFactory moleFactory)
        {
            _holeGrid = holeGrid;
            _moleFactory = moleFactory;
        }

        public void AddHole(int rows, int columns)
        {
            int index = _moles.Count;
            Transform hole = _holeGrid.CreateHole(index, rows, columns);

            _moles.Add(_moleFactory.Create(hole, index + 1));
            _timers.Add(0f);
        }

        public bool TryShowInRandomFreeHole(float duration)
        {
            _freeIndices.Clear();

            for (int i = 0; i < _moles.Count; i++)
            {
                if (_moles[i].IsVisible == false)
                    _freeIndices.Add(i);
            }

            if (_freeIndices.Count == 0)
                return false;

            int index = _freeIndices[Random.Range(0, _freeIndices.Count)];

            _timers[index] = duration;
            _moles[index].Show();

            return true;
        }

        public bool TryWhack(int index)
        {
            if (index < 0 || index >= _moles.Count)
                return false;

            if (_moles[index].IsVisible == false)
                return false;

            _moles[index].Hide();
            _timers[index] = 0f;

            return true;
        }

        public int TickTimers(float deltaTime)
        {
            int expiredCount = 0;

            for (int i = 0; i < _moles.Count; i++)
            {
                if (_moles[i].IsVisible == false)
                    continue;

                _timers[i] -= deltaTime;

                if (_timers[i] > 0f)
                    continue;

                _moles[i].Hide();
                _timers[i] = 0f;
                expiredCount++;
            }

            return expiredCount;
        }

        public void HideAll()
        {
            for (int i = 0; i < _moles.Count; i++)
            {
                _moles[i].Hide();
                _timers[i] = 0f;
            }
        }
    }
}
