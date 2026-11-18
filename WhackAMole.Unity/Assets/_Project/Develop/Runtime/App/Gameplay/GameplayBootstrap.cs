using _Project.Develop.Runtime.Gameplay.Moles;
using _Project.Develop.Runtime.Gameplay.Run;
using VContainer.Unity;

namespace _Project.Develop.Runtime.App.Gameplay
{
    // Пока переход синхронный, сцена запускает себя сама: когда переход научится ждать её готовности, запускать будет он
    public sealed class GameplayBootstrap : IStartable
    {
        private readonly MoleField _moleField;
        private readonly RunSession _runSession;

        public GameplayBootstrap(MoleField moleField, RunSession runSession)
        {
            _moleField = moleField;
            _runSession = runSession;
        }

        public void Start()
        {
            _moleField.Build();
            _runSession.Start();
        }
    }
}
