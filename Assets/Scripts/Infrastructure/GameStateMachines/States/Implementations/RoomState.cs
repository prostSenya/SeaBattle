using Core.Services.SceneObjectResolvers;
using Infrastructure.GameStateMachines.States.Interfaces;
using UI.LobbyPanel.Presenters;
using UI.RoomPanel.Presenters;
using VContainer;

namespace Infrastructure.GameStateMachines.States.Implementations
{
    public sealed class RoomState : IState
    {
        private readonly ISceneScopeResolverProxy _scopeResolverProxy;

        private IRoomPresenter _roomPresenter;

        public RoomState(ISceneScopeResolverProxy scopeResolverProxy)
        {
            _scopeResolverProxy = scopeResolverProxy;
        }
        
        public void Enter()
        {
            _roomPresenter = _scopeResolverProxy.Resolver.Resolve<IRoomPresenter>();
            _roomPresenter.Enable();
        }

        public void Exit()
        {
            _roomPresenter.Disable();
        }
    }
}