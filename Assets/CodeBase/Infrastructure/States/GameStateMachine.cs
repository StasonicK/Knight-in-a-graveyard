using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        // [Inject]
        private ISceneLoader _sceneLoader;
        [Inject] private IGameFactory _gameFactory;
        [Inject] private IPersistentProgressService _progressService;
        [Inject] private IStaticDataService _staticDataService;
        [Inject] private IUIFactory _uiFactory;
        [Inject] private ISaveLoadService _saveLoadService;

        // [Inject]
        public GameStateMachine(
            ISceneLoader sceneLoader, 
            ILoadingCurtain loadingCurtain
            // , IGameFactory gameFactory, IPersistentProgressService progressService,
            // IStaticDataService staticDataService, IUIFactory uiFactory, ISaveLoadService saveLoadService
            )
        {
            _sceneLoader = sceneLoader;
            
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(_sceneLoader),
                [typeof(LoadLevelState)] =
                    new LoadLevelState(this, _sceneLoader, loadingCurtain, _gameFactory, _progressService, _staticDataService, _uiFactory),
                [typeof(LoadProgressState)] = new LoadProgressState(this, _progressService, _saveLoadService),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}