using System;
using _Project.Develop.Runtime.App.Input;
using _Project.Develop.Runtime.App.Navigation;
using _Project.Develop.Runtime.Gameplay.Levels;
using _Project.Develop.Runtime.Gameplay.Run;

namespace _Project.Develop.Runtime.App.Gameplay
{
    public sealed class RunEndInput : IRunEndControls
    {
        private readonly KeyboardInput _input;
        private readonly GameNavigation _gameNavigation;
        private readonly CurrentLevel _currentLevel;

        private Action _confirm;
        private Action _back;

        public RunEndInput(
            KeyboardInput input,
            GameNavigation gameNavigation,
            CurrentLevel currentLevel)
        {
            _input = input;
            _gameNavigation = gameNavigation;
            _currentLevel = currentLevel;
        }

        public void BindVictory()
        {
            _confirm = _gameNavigation.OpenMainMenu;
            _back = null;
        }

        public void BindDefeat()
        {
            _confirm = () => _gameNavigation.OpenGameplay(_currentLevel.Number);
            _back = _gameNavigation.OpenMainMenu;
        }

        public void Clear()
        {
            _confirm = null;
            _back = null;
        }

        public void Tick()
        {
            if (_confirm != null && _input.WasConfirmPressed())
                _confirm();
            else if (_back != null && _input.WasBackPressed())
                _back();
        }
    }
}
