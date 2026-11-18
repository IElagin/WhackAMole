using VContainer.Unity;

namespace _Project.Develop.Runtime.App.MainMenu
{
    // Пока переход синхронный, сцена запускает себя сама: когда переход научится ждать её готовности, запускать будет он
    public sealed class MainMenuBootstrap : IStartable
    {
        private readonly MainMenuScreen _screen;

        public MainMenuBootstrap(MainMenuScreen screen)
        {
            _screen = screen;
        }

        public void Start()
        {
            _screen.ShowStatus();
        }
    }
}
