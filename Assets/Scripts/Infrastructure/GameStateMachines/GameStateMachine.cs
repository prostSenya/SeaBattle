using System;
using Core.Services.StateFactories;
using Infrastructure.GameStateMachines.States.Interfaces;
using UnityEngine;

namespace Infrastructure.GameStateMachines
{
    public class GameStateMachine :IStateMachine
    {
        private readonly IStateFactory _stateFactory;
        private IExitableState _state;

        public GameStateMachine(IStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }
        
        public void ChangeState<T>() where T : IExitableState
        {
            _state?.Exit();
            _state = _stateFactory.Create<T>();
            
            IState newState = _state is IState ? (IState) _state : throw new Exception($"InvalidState {_state} in GameStateMachine");

            newState.Enter();
        }
        
        public void ChangeState<T, TPayload>(TPayload payload) where T : IExitableState
        {
            _state?.Exit();
            _state = _stateFactory.Create<T>();
            IPayloadState<TPayload> newState = _state is IPayloadState<TPayload> ? (IPayloadState<TPayload> ) _state : throw new Exception($"InvalidState {_state} in GameStateMachine");
            newState.Enter(payload);
        }
    }
}