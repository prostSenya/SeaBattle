using Infrastructure.GameStateMachines.States.Interfaces;
using VContainer;

namespace Core.Services.StateFactories
{
    public class StateFactory : IStateFactory
    {
        private IObjectResolver _resolver;

        public StateFactory(IObjectResolver  resolver) => 
            _resolver = resolver;

        public IExitableState Create<T>() where T : IExitableState => 
            _resolver.Resolve<T>();
    }
}