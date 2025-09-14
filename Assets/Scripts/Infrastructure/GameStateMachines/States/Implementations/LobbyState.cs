using System.Collections.Generic;
using Core.Services.SceneObjectResolvers;
using Fusion;
using Infrastructure.GameStateMachines.States.Interfaces;
using Network;
using Network.LobbyServices;
using UI.LobbyPanel.Presenters;
using UnityEngine;
using VContainer;

namespace Infrastructure.GameStateMachines.States.Implementations
{
    public class LobbyState : IState
    {
        private readonly ISceneScopeResolverProxy _sceneScopeResolverProxy;
        private readonly IFusionCallbacks _fusionCallbacks;
        private readonly ILobbyService _lobbyService;

        private ILobbyPresenter _lobbyPresenter;

        public LobbyState(
            ILobbyService lobbyService,
            IFusionCallbacks fusionCallbacks,
            ISceneScopeResolverProxy sceneScopeResolverProxy)
        {
            _sceneScopeResolverProxy = sceneScopeResolverProxy;
            _lobbyService = lobbyService;
            _fusionCallbacks = fusionCallbacks;
        }
        
        public async void Enter()
        {
            Debug.Log("Entered Lobby");
            _fusionCallbacks.SessionListUpdated += OnSessionUpdated;
            _lobbyPresenter = _sceneScopeResolverProxy.Resolver.Resolve<ILobbyPresenter>();
            _lobbyPresenter.EnableLoadingAnimation();
            await _lobbyService.JoinToLobbySession(SessionLobby.ClientServer);
        }

        public void Exit()
        {
            _fusionCallbacks.SessionListUpdated -= OnSessionUpdated;
            _lobbyPresenter.Dispose();
        }

        private void OnSessionUpdated(IReadOnlyList<SessionInfo> sessionInfos)
        {
            Debug.Log($"{sessionInfos.Count} sessions updated");
            _lobbyPresenter.DisableLoadingAnimation();
            _lobbyPresenter.UpdateLobbys(sessionInfos);
        }
    }
}