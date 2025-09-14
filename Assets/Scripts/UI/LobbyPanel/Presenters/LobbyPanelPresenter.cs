using System;
using Core.Services.SceneLoaders;
using Cysharp.Threading.Tasks;
using Fusion;
using Infrastructure.GameStateMachines;
using Infrastructure.GameStateMachines.States.Implementations;
using Network.LobbyServices;
using Network.NetworkRunnerProvider;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

namespace UI.LobbyPanel
{
    public class LobbyPanelPresenter : ILobbyPanelPresenter
    {
        private readonly ILobbyService _lobbyService;
        private readonly IStateMachine _stateMachine;
        private readonly INetworkRunnerProvider _networkRunnerProvider;

        private SessionInfo _sessionInfo;

        public LobbyPanelPresenter(
            SessionInfo sessionInfo,
            ILobbyService lobbyService,
            IStateMachine stateMachine,
            INetworkRunnerProvider networkRunnerProvider)
        {
            _sessionInfo = sessionInfo;
            _lobbyService = lobbyService;
            _stateMachine = stateMachine;
            _networkRunnerProvider = networkRunnerProvider;
        }

        public event Action<int> PlayerCountChanged;
        public event Action<string> RoomNameChanged;

        public string RoomName =>
            _sessionInfo.Properties != null &&
            _sessionInfo.Properties.TryGetValue("displayName", out SessionProperty sessionProperty)
                ? sessionProperty.ToString()
                : _sessionInfo.Name;

        public int PlayerCount => _sessionInfo.PlayerCount;

        public async void JoinToRoom()
        {
            try
            {
                await _lobbyService.JoinByNameAsync(_sessionInfo.Name);
            }
            catch (Exception e)
            {
                throw new Exception($"Cant join room by name - {_sessionInfo.Name} {e}");
            }

            SceneRef sceneRef = SceneRef.FromIndex(SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Room.unity"));

            try
            {
                await _networkRunnerProvider.Runner.LoadScene(sceneRef).ToUniTask();
                _stateMachine.ChangeState<RoomState>();
            }
            catch (Exception e)
            {
                throw new  Exception("Error loading scene", e);
            }
        }

        public void UpdateSessionData(SessionInfo sessionInfo)
        {
            bool nameChanged = _sessionInfo.Name != sessionInfo.Name;
            bool countChanged = _sessionInfo.PlayerCount != sessionInfo.PlayerCount;

            _sessionInfo = sessionInfo;

            if (nameChanged) RoomNameChanged?.Invoke(_sessionInfo.Name);
            if (countChanged) PlayerCountChanged?.Invoke(_sessionInfo.PlayerCount);
        }
    }
}