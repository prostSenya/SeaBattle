using Infrastructure.GameStateMachines.States.Interfaces;

namespace Infrastructure.GameStateMachines
{
    public interface IStateMachine
    {
        public void ChangeState<T>() where T : IExitableState;
        public void ChangeState<T, TPayload>(TPayload payload) where T : IExitableState;
    }
}