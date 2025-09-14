namespace Infrastructure.GameStateMachines.States.Interfaces
{
    public interface IPayloadState<T> : IExitableState
    {
        void Enter(T payload);
    }
}