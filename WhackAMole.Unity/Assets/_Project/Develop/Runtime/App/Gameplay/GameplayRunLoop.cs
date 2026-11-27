using System;
using _Project.Develop.Runtime.Gameplay.Moles;
using _Project.Develop.Runtime.Gameplay.Run;
using UnityEngine;
using VContainer.Unity;

namespace _Project.Develop.Runtime.App.Gameplay
{
    public sealed class GameplayRunLoop : ITickable, IDisposable
    {
        private readonly RunStatesFactory _statesFactory;
        private readonly RunSession _session;
        private readonly MoleSpawner _spawner;
        private readonly WhackInput _whackInput;
        private readonly RunEndInput _runEndInput;

        private RunStateMachine _machine;

        public GameplayRunLoop(
            RunStatesFactory statesFactory, RunSession session, MoleSpawner spawner, WhackInput whackInput,
            RunEndInput runEndInput)
        {
            _statesFactory = statesFactory;
            _session = session;
            _spawner = spawner;
            _whackInput = whackInput;
            _runEndInput = runEndInput;
        }

        public RunState CurrentState => _machine?.ActiveState;

        public void Run()
        {
            if (_machine != null)
                return;

            RunStateMachine machine = _statesFactory.Create();
            machine.Enter();
            _machine = machine;
        }

        public void Tick()
        {
            if (_machine == null)
                return;

            float deltaTime = Time.deltaTime;

            if (CurrentState is PlayingRunState)
            {
                _spawner.Tick(deltaTime);

                if (_session.HasLives)
                    _whackInput.Tick();
            }

            // The FSM checks transitions before UpdateLogic, so feed it this frame's results first.
            _machine.Update(deltaTime);
            _runEndInput.Tick(CurrentState);
        }

        public void Dispose()
        {
            if (_machine == null)
                return;

            _machine.Exit();
            _machine.Dispose();
            _machine = null;
        }
    }
}
