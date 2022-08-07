public interface IState : IExitableState
{
    void Enter();
}

public interface IPayloadedState<Tpayload> : IExitableState
{
    void Enter(Tpayload payload);
}

public interface IExitableState
{
    void Exit();
}