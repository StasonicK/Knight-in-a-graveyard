using System;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        public event Action Entered;

        // private readonly GameStateMachine _stateMachine;


        public GameLoopState(
            // GameStateMachine stateMachine
            )
        {
            // _stateMachine = stateMachine;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}