namespace _Project.Develop.Runtime.Gameplay.Run
{
    public sealed class WonRunState : RunState
    {
        private readonly RunSession _session;
        private readonly RunFinisher _finisher;

        public WonRunState(RunSession session, RunFinisher finisher)
        {
            _session = session;
            _finisher = finisher;
        }

        public override void Enter()
        {
            base.Enter();

            _finisher.Win(_session.Gold);
        }
    }
}
