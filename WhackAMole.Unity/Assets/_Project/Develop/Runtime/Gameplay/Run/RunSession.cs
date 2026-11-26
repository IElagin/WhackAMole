using _Project.Develop.Runtime.Gameplay.Levels;

namespace _Project.Develop.Runtime.Gameplay.Run
{
    public sealed class RunSession
    {
        private readonly CurrentLevel _currentLevel;
        private readonly RunFinisher _runFinisher;
        private readonly GameplayConsole _console;

        public RunSession(CurrentLevel currentLevel, RunFinisher runFinisher, GameplayConsole console)
        {
            _currentLevel = currentLevel;
            _runFinisher = runFinisher;
            _console = console;
        }

        public RunState State { get; private set; } = RunState.NotStarted;

        public int Hits { get; private set; }

        public int Lives { get; private set; }

        public int Gold { get; private set; }

        public void Start()
        {
            LevelConfig config = _currentLevel.Config;

            Hits = 0;
            Gold = 0;
            Lives = config.Lives;
            State = RunState.Playing;

            _console.RunStarted(_currentLevel.Number, config.RequiredHits, Lives, config.Rows * config.Columns);
        }

        public void AddHit()
        {
            if (State != RunState.Playing)
                return;

            LevelConfig config = _currentLevel.Config;

            Hits++;
            Gold += config.GoldPerHit;

            _console.Hit(Hits, config.RequiredHits, Gold);

            if (Hits < config.RequiredHits)
                return;

            State = RunState.Won;
            _runFinisher.Win(Gold);
        }

        public void LoseLife()
        {
            if (State != RunState.Playing)
                return;

            Lives--;

            _console.LifeLost(Lives);

            if (Lives > 0)
                return;

            State = RunState.Lost;
            _runFinisher.Lose(Gold);
        }
    }
}
