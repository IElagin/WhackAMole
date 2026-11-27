namespace _Project.Develop.Runtime.Gameplay.Run
{
    public sealed class WonRunState : RunState
    {
        private readonly RunSession _session;
        private readonly RunFinisher _finisher;
        private readonly IRunEndControls _controls;

        public WonRunState(RunSession session, RunFinisher finisher, IRunEndControls controls)
        {
            _session = session;
            _finisher = finisher;
            _controls = controls;
        }

        public override void Enter()
        {
            base.Enter();

            _finisher.Win(_session.Gold);
            _controls.BindVictory();
        }

        public override void Exit()
        {
            _controls.Clear();

            base.Exit();
        }
    }
}
