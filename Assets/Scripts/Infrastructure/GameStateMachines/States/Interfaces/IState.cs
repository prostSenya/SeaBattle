namespace Infrastructure.GameStateMachines.States.Interfaces
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}