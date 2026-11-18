using _Project.Develop.Runtime.App.Input;
using VContainer.Unity;

namespace _Project.Develop.Runtime.App.MainMenu
{
    public sealed class MainMenuInput : ITickable
    {
        private readonly KeyboardInput _input;
        private readonly LevelSelector _levelSelector;
        private readonly MainMenuScreen _screen;

        public MainMenuInput(KeyboardInput input, LevelSelector levelSelector, MainMenuScreen screen)
        {
            _input = input;
            _levelSelector = levelSelector;
            _screen = screen;
        }

        public void Tick()
        {
            if (_input.TryGetPressedNumber(out int number) == false)
                return;

            if (_levelSelector.Select(number) == LevelSelectResult.Locked)
                _screen.ShowLevelLocked(number);
        }
    }
}
