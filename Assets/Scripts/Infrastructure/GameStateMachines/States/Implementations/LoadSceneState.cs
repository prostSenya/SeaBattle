using Core.Services.SceneLoaders;
using Infrastructure.GameStateMachines.States.Interfaces;

namespace Infrastructure.GameStateMachines.States.Implementations
{
    public class LoadSceneState : IPayloadState<string>
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IStateMachine _stateMachine;

        public LoadSceneState(ISceneLoader sceneLoader, IStateMachine stateMachine)
        {
            _sceneLoader = sceneLoader;
            _stateMachine = stateMachine;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.SceneLoaded += OnSceneLoaded;
            _sceneLoader.LoadScene(sceneName);
        }

        public void Exit()
        {
        }

        private void OnSceneLoaded()
        {
            _sceneLoader.SceneLoaded -= OnSceneLoaded;
            _stateMachine.ChangeState<MainMenuState>();
        }
    }
}