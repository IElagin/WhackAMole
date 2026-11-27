namespace _Project.Develop.Runtime.Gameplay.Run
{
    public sealed class LostRunState : RunState
    {
        private readonly RunSession _session;
        private readonly RunFinisher _finisher;

        public LostRunState(RunSession session, RunFinisher finisher)
        {
            _session = session;
            _finisher = finisher;
        }

        public override void Enter()
        {
            base.Enter();

            _finisher.Lose(_session.Gold);
        }
    }
}
