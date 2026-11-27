using _Project.Develop.Runtime.Gameplay.Run;

namespace _Project.Develop.Runtime.Gameplay.Moles
{
    public sealed class WhackService
    {
        private readonly MoleField _field;
        private readonly RunSession _session;
        private readonly GameplayConsole _console;

        public WhackService(MoleField field, RunSession session, GameplayConsole console)
        {
            _field = field;
            _session = session;
            _console = console;
        }

        public bool TryWhack(int index)
        {
            if (_field.TryWhack(index) == false)
                return false;

            _session.AddHit();
            _console.Hit(_session.Hits, _session.RequiredHits, _session.Gold);
            return true;
        }
    }
}
