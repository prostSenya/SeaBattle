using Infrastructure.GameStateMachines;
using Infrastructure.GameStateMachines.States.Implementations;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.DI.ProjectContext.Initializers
{
    public class ProjectInitializer : MonoBehaviour, IInitializable
    {
        private IStateMachine _stateMachine;

        [Inject]
        private void Construct(IStateMachine stateMachine) => 
            _stateMachine = stateMachine;

        public void Initialize() => 
            _stateMachine.ChangeState<BootState>();
    }
}