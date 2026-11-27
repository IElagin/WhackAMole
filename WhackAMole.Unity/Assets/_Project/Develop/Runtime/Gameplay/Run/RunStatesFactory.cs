using _Project.Develop.Runtime.Gameplay.Levels;
using Assets._Project.Develop.Runtime.Utilities.Conditions;

namespace _Project.Develop.Runtime.Gameplay.Run
{
    public sealed class RunStatesFactory
    {
        private readonly CurrentLevel _currentLevel;
        private readonly RunSession _session;
        private readonly RunFinisher _finisher;
        private readonly GameplayConsole _console;
        private readonly IRunEndControls _controls;

        public RunStatesFactory(
            CurrentLevel currentLevel, RunSession session, RunFinisher finisher, GameplayConsole console,
            IRunEndControls controls)
        {
            _currentLevel = currentLevel;
            _session = session;
            _finisher = finisher;
            _console = console;
            _controls = controls;
        }

        public RunStateMachine Create()
        {
            PlayingRunState playing = new(_currentLevel, _session, _console);
            WonRunState won = new(_session, _finisher, _controls);
            LostRunState lost = new(_session, _finisher, _controls);

            RunStateMachine machine = new();
            machine.AddState(playing);
            machine.AddState(won);
            machine.AddState(lost);

            machine.AddTransition(playing, lost, new FuncCondition(() => _session.HasLives == false));
            machine.AddTransition(playing, won, new FuncCondition(() => _session.HasRequiredHits));

            return machine;
        }
    }
}
