using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;

namespace _Project.Develop.Runtime.Gameplay.Run
{
    public sealed class RunStateMachine : StateMachine<RunState>
    {
        public RunStateMachine() : base(new()) { }

        public RunState ActiveState => CurrentState;
    }
}
