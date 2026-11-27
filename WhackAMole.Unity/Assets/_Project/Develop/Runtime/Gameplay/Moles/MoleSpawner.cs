using _Project.Develop.Runtime.Gameplay.Levels;
using _Project.Develop.Runtime.Gameplay.Run;

namespace _Project.Develop.Runtime.Gameplay.Moles
{
    public sealed class MoleSpawner
    {
        private readonly MoleField _moleField;
        private readonly RunSession _runSession;
        private readonly CurrentLevel _currentLevel;
        private readonly GameplayConsole _console;

        private float _spawnTimer;

        public MoleSpawner(MoleField moleField, RunSession runSession, CurrentLevel currentLevel, GameplayConsole console)
        {
            _moleField = moleField;
            _runSession = runSession;
            _currentLevel = currentLevel;
            _console = console;
        }

        public void Tick(float deltaTime)
        {
            int expiredCount = _moleField.TickTimers(deltaTime);

            for (int i = 0; i < expiredCount; i++)
            {
                _runSession.LoseLife();
                _console.LifeLost(_runSession.Lives);

                if (_runSession.HasLives == false)
                    return;
            }

            _spawnTimer += deltaTime;

            LevelConfig config = _currentLevel.Config;

            if (_spawnTimer < config.SpawnInterval)
                return;

            _spawnTimer = 0f;
            _moleField.TryShowInRandomFreeHole(config.VisibleDuration);
        }
    }
}
