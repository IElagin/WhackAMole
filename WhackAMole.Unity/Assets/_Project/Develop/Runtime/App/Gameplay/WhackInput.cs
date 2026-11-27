using _Project.Develop.Runtime.App.Input;
using _Project.Develop.Runtime.Gameplay;
using _Project.Develop.Runtime.Gameplay.Moles;
using _Project.Develop.Runtime.Gameplay.Run;

namespace _Project.Develop.Runtime.App.Gameplay
{
    public sealed class WhackInput
    {
        private readonly KeyboardInput _input;
        private readonly MoleField _moleField;
        private readonly RunSession _runSession;
        private readonly GameplayConsole _console;

        public WhackInput(KeyboardInput input, MoleField moleField, RunSession runSession, GameplayConsole console)
        {
            _input = input;
            _moleField = moleField;
            _runSession = runSession;
            _console = console;
        }

        public void Tick()
        {
            if (_input.TryGetPressedNumber(out int number) == false)
                return;

            if (_moleField.TryWhack(number - 1) == false)
                return;

            _runSession.AddHit();
            _console.Hit(_runSession.Hits, _runSession.RequiredHits, _runSession.Gold);
        }
    }
}
