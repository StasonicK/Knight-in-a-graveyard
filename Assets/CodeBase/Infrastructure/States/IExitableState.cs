using System;

namespace CodeBase.Infrastructure.States
{
    public interface IExitableState
    {
        void Exit();
        event Action Entered;
    }
}