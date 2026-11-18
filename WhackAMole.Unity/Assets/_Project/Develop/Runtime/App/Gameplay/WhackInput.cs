using _Project.Develop.Runtime.App.Input;
using _Project.Develop.Runtime.Gameplay.Moles;
using _Project.Develop.Runtime.Gameplay.Run;
using VContainer.Unity;

namespace _Project.Develop.Runtime.App.Gameplay
{
    public sealed class WhackInput : ITickable
    {
        private readonly KeyboardInput _input;
        private readonly MoleField _moleField;
        private readonly RunSession _runSession;

        public WhackInput(KeyboardInput input, MoleField moleField, RunSession runSession)
        {
            _input = input;
            _moleField = moleField;
            _runSession = runSession;
        }

        public void Tick()
        {
            if (_runSession.State != RunState.Playing)
                return;

            if (_input.TryGetPressedNumber(out int number) == false)
                return;

            if (_moleField.TryWhack(number - 1))
                _runSession.AddHit();
        }
    }
}
