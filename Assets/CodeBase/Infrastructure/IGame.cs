using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure
{
    public interface IGame
    {
        IGameStateMachine GetGameStateMachine();
    }
}