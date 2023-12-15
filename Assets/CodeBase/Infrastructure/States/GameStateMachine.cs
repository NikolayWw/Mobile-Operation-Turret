using CodeBase.Infrastructure.Logic;
using CodeBase.Services;
using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitable> _states;
        private IExitable _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadCurtain loadCurtain, AllServices services)
        {
            _states = new Dictionary<Type, IExitable>
            {
                [typeof(RegisterServiceState)] = new RegisterServiceState(this, services),
                [typeof(WarmupState)] = new WarmupState(services),
                [typeof(LoadLevelState)] = new LoadLevelState(sceneLoader, loadCurtain, services),
                [typeof(LoopState)] = new LoopState(),
            };
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IExitable
        {
            _activeState?.Exit();
            TState state = _states[typeof(TState)] as TState;
            _activeState = state;
            return state;
        }
    }
}