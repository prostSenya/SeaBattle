using Core.Services.SceneLoaders;
using Fusion;
using Infrastructure.GameStateMachines;
using Infrastructure.GameStateMachines.States.Implementations;
using Network.LobbyServices;
using Network.NetworkRunnerFactories;
using UnityEngine;

namespace UI.MainMenuWindows
{
    public class MainMenuPresenter : IMainMenuPresenter
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IStateMachine _stateMachine;

        public MainMenuPresenter(
            ISceneLoader sceneLoader,
            IStateMachine stateMachine)
        {
            _sceneLoader = sceneLoader;
            _stateMachine = stateMachine;
        }

        public void ExitApplication() => 
            Application.Quit();

        public async void GoToLobby()
        {
            _sceneLoader.SceneLoaded += OnSceneLoaded;
            
            await _sceneLoader.LoadScene("Lobby", false, true);
        }

        private void OnSceneLoaded()
        {
            Debug.Log("Start OnSceneLoaded");
            _stateMachine.ChangeState<LobbyState>();
            Debug.Log("Middle OnSceneLoaded");
            _sceneLoader.SceneLoaded -= OnSceneLoaded;
            Debug.Log("End OnSceneLoaded");
        }
    }
}