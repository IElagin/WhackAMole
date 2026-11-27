namespace _Project.Develop.Runtime.Gameplay.Run
{
    public sealed class LostRunState : RunState
    {
        private readonly RunSession _session;
        private readonly RunFinisher _finisher;
        private readonly IRunEndControls _controls;

        public LostRunState(RunSession session, RunFinisher finisher, IRunEndControls controls)
        {
            _session = session;
            _finisher = finisher;
            _controls = controls;
        }

        public override void Enter()
        {
            base.Enter();

            _finisher.Lose(_session.Gold);
            _controls.BindDefeat();
        }

        public override void Exit()
        {
            _controls.Clear();

            base.Exit();
        }
    }
}
