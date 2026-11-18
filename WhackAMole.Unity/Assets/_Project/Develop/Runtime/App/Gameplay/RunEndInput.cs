using _Project.Develop.Runtime.App.Input;
using _Project.Develop.Runtime.App.Navigation;
using _Project.Develop.Runtime.Gameplay.Levels;
using _Project.Develop.Runtime.Gameplay.Run;
using VContainer.Unity;

namespace _Project.Develop.Runtime.App.Gameplay
{
    public sealed class RunEndInput : ITickable
    {
        private readonly KeyboardInput _input;
        private readonly RunSession _runSession;
        private readonly GameNavigation _gameNavigation;
        private readonly CurrentLevel _currentLevel;

        public RunEndInput(
            KeyboardInput input,
            RunSession runSession,
            GameNavigation gameNavigation,
            CurrentLevel currentLevel)
        {
            _input = input;
            _runSession = runSession;
            _gameNavigation = gameNavigation;
            _currentLevel = currentLevel;
        }

        public void Tick()
        {
            switch (_runSession.State)
            {
                case RunState.Won:
                    if (_input.WasConfirmPressed())
                        _gameNavigation.OpenMainMenu();

                    break;

                case RunState.Lost:
                    if (_input.WasConfirmPressed())
                        _gameNavigation.OpenGameplay(_currentLevel.Number);
                    else if (_input.WasBackPressed())
                        _gameNavigation.OpenMainMenu();

                    break;
            }
        }
    }
}
