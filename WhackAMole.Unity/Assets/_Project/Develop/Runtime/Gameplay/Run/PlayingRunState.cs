using _Project.Develop.Runtime.Gameplay.Levels;

namespace _Project.Develop.Runtime.Gameplay.Run
{
    public sealed class PlayingRunState : RunState
    {
        private readonly CurrentLevel _currentLevel;
        private readonly RunSession _session;
        private readonly GameplayConsole _console;

        public PlayingRunState(CurrentLevel currentLevel, RunSession session, GameplayConsole console)
        {
            _currentLevel = currentLevel;
            _session = session;
            _console = console;
        }

        public override void Enter()
        {
            base.Enter();

            _session.Reset();
            LevelConfig config = _currentLevel.Config;
            _console.RunStarted(_currentLevel.Number, config.RequiredHits, _session.Lives, config.Rows * config.Columns);
        }
    }
}
