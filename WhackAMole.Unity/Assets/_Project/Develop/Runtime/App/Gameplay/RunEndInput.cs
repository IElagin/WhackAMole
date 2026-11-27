using _Project.Develop.Runtime.App.Input;
using _Project.Develop.Runtime.App.Navigation;
using _Project.Develop.Runtime.Gameplay.Levels;
using _Project.Develop.Runtime.Gameplay.Run;

namespace _Project.Develop.Runtime.App.Gameplay
{
    public sealed class RunEndInput
    {
        private readonly KeyboardInput _input;
        private readonly GameNavigation _gameNavigation;
        private readonly CurrentLevel _currentLevel;

        public RunEndInput(
            KeyboardInput input,
            GameNavigation gameNavigation,
            CurrentLevel currentLevel)
        {
            _input = input;
            _gameNavigation = gameNavigation;
            _currentLevel = currentLevel;
        }

        public void Tick(RunState state)
        {
            switch (state)
            {
                case WonRunState:
                    if (_input.WasConfirmPressed())
                        _gameNavigation.OpenMainMenu();

                    break;

                case LostRunState:
                    if (_input.WasConfirmPressed())
                        _gameNavigation.OpenGameplay(_currentLevel.Number);
                    else if (_input.WasBackPressed())
                        _gameNavigation.OpenMainMenu();

                    break;
            }
        }
    }
}
