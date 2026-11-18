using _Project.Develop.Runtime.Gameplay.Levels;
using _Project.Develop.Runtime.Gameplay.Run;
using UnityEngine;
using VContainer.Unity;

namespace _Project.Develop.Runtime.Gameplay.Moles
{
    public sealed class MoleSpawner : ITickable
    {
        private readonly MoleField _moleField;
        private readonly RunSession _runSession;
        private readonly CurrentLevel _currentLevel;

        private float _spawnTimer;
        private bool _isFieldCleared;

        public MoleSpawner(MoleField moleField, RunSession runSession, CurrentLevel currentLevel)
        {
            _moleField = moleField;
            _runSession = runSession;
            _currentLevel = currentLevel;
        }

        public void Tick()
        {
            if (_runSession.State == RunState.Won || _runSession.State == RunState.Lost)
            {
                ClearField();
                return;
            }

            if (_runSession.State != RunState.Playing)
                return;

            int expiredCount = _moleField.TickTimers(Time.deltaTime);

            for (int i = 0; i < expiredCount; i++)
            {
                _runSession.LoseLife();

                if (_runSession.State != RunState.Playing)
                {
                    ClearField();
                    return;
                }
            }

            _spawnTimer += Time.deltaTime;

            if (_spawnTimer < _currentLevel.Config.SpawnInterval)
                return;

            _spawnTimer = 0f;
            _moleField.TryShowInRandomFreeHole(_currentLevel.Config.VisibleDuration);
        }

        private void ClearField()
        {
            if (_isFieldCleared)
                return;

            _moleField.HideAll();
            _isFieldCleared = true;
        }
    }
}
