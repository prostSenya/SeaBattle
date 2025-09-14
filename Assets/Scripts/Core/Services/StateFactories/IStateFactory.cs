using Infrastructure.GameStateMachines.States.Interfaces;

namespace Core.Services.StateFactories
{
    public interface IStateFactory
    {
        IExitableState Create<T>() where T : IExitableState;
    }
}