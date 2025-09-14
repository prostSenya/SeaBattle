using Core.Services.SceneLoaders;
using Infrastructure.GameStateMachines.States.Interfaces;

namespace Infrastructure.GameStateMachines.States.Implementations
{
    public sealed class BootState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;

        public BootState(IStateMachine stateMachine, ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Exit()
        { }

        public async void Enter()
        {
            _sceneLoader.SceneLoaded += OnSceneLoaded; 
            await _sceneLoader.LoadScene("MainMenu");
        }

        private void OnSceneLoaded()
        {
            _sceneLoader.SceneLoaded -= OnSceneLoaded; 
            _stateMachine.ChangeState<MainMenuState>();
        }
    }
}