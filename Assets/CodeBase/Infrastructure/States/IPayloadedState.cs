public interface IPayloadedState<Tpayload> : IExitableState
{
    void Enter(Tpayload payload);
}