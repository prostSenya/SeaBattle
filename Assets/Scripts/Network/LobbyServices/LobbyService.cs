using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Fusion;
using Network.NetworkRunnerFactories;
using Network.NetworkRunnerProvider;
using UnityEngine;

namespace Network.LobbyServices
{
    public class LobbyService : ILobbyService
    {
        private readonly INetworkRunnerProvider _networkRunnerProvider;
        private readonly INetworkRunnerFactory _networkRunnerFactory;

        public event Action JoinedToLobbySession;
        
        public LobbyService(INetworkRunnerProvider networkRunnerProvider, INetworkRunnerFactory  networkRunnerFactory)
        {
            _networkRunnerProvider = networkRunnerProvider;
            _networkRunnerFactory = networkRunnerFactory;
        }

        public async Task CreateAndHostAsync(NetworkSceneInfo sceneBuildIndex = default)
        {
            NetworkRunner runner = _networkRunnerProvider.Runner;

            if (!runner)
            {
                Debug.LogError("No network runner found");
                runner =  _networkRunnerFactory.Create();
            }
            
            // Dictionary<string, SessionProperty> props = new Dictionary<string, SessionProperty>
            // {
            //     ["rid"] = Guid.NewGuid().ToString(), // стабильный ID
            //     ["display"] = "EU #12  |  Casual"    // красивое имя для UI
            // };
             
            var args = new StartGameArgs()
            {
                GameMode = GameMode.AutoHostOrClient,
                //SessionName = "sessionName",// запоминаем нахуй. Если не указывать имя Сессии. Фотон сам сгенерит. Это имя будет выглядеть как гуид
                PlayerCount = 2,
                SceneManager =  _networkRunnerProvider.Runner.GetComponent<NetworkSceneManagerDefault>()
                //SessionProperties = props
            };

            StartGameResult result = await runner.StartGame(args);

            if (result.Ok == false)
            {
                Debug.LogError($"Failed to start game: {result.ErrorMessage}");
                await LeaveAsync();
            }
        }
        
        /// <summary>
        /// пример из документации Fusion
        /// </summary>
        public async Task StartHost() {

            var result = await _networkRunnerProvider.Runner.StartGame(new StartGameArgs() {
                GameMode = GameMode.Host,
                CustomLobbyName = "MyCustomLobby"
            });

            if (result.Ok) {
                // all good
            } else {
                Debug.LogError($"Failed to Start: {result.ShutdownReason}");
            }
        }

        public async Task JoinByNameAsync(string sessionName)
        {
            StartGameResult result = await _networkRunnerProvider.Runner.StartGame(new StartGameArgs
            {
                GameMode    = GameMode.Client,
                SessionName = sessionName,
            });

            if (!result.Ok)
                Debug.LogError($"Failed to join '{sessionName}': {result.ShutdownReason} / {result.ErrorMessage}");        }

        public async Task JoinRandomAsync() => 
            Debug.Log($"Joining random game");

        public async Task LeaveAsync() => 
            await _networkRunnerFactory.DestroyAsync();
/// <summary>
/// только позволяет смотреть открытые комнаты
/// </summary>
/// <param name="sessionLobby"></param>
/// <exception cref="Exception"></exception>
        public async UniTask JoinToLobbySession(SessionLobby sessionLobby)
        {
            NetworkRunner runner = _networkRunnerProvider.Runner;
Debug.Log("JoinToLobbySession");
            if (!runner)
            {
                Debug.LogError("No network runner found");
                runner =  _networkRunnerFactory.Create();
            }

            StartGameResult result = await runner.JoinSessionLobby(sessionLobby).AsUniTask();

            if (result.Ok == false) 
                throw new Exception("Failed to join lobby");
            
            JoinedToLobbySession?.Invoke();
        }
    }
}