using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class GameStateMachine
    {
        // private readonly DiContainer _diContainer;

        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        // [Inject] private ILoadingCurtain _loadingCurtain;
        // [Inject] private ISceneLoader _sceneLoader;
        // [Inject] private IGameFactory _gameFactory;
        // [Inject] private IStaticDataService _staticDataService;
        // [Inject] private IUIFactory _uiFactory;
        // [Inject] private ISaveLoadService _saveLoadService;
        
        // [Inject]
        private IPersistentProgressService _progressService;

        // [Inject(Id = CodeBase.States.BootstrapState)]
        // private IExitableState _bootstrapState;
        //
        // [Inject(Id = CodeBase.States.LoadLevelState)]
        // private IExitableState _loadLevelState;
        //
        // [Inject(Id = CodeBase.States.LoadProgressState)]
        // private IExitableState _loadProgressState;
        //
        // [Inject(Id = CodeBase.States.GameLoopState)]
        // private IExitableState _gameLoopState;

        [Inject]
        public GameStateMachine(
            IPersistentProgressService progressService,
            // IEnumerable<IExitableState> states,
            DiContainer diContainer
        )
        {
            _progressService = progressService;
            List<IExitableState> _states = diContainer.ResolveAll<IExitableState>();
            // List<IExitableState> _states = states;
            // _diContainer = diContainer;
            // _states = new Dictionary<Type, IExitableState>()
            // {
            //     [typeof(BootstrapState)] = _bootstrapState,
            //     [typeof(LoadLevelState)] = _loadLevelState,
            //     [typeof(LoadProgressState)] = _loadProgressState,
            //     [typeof(GameLoopState)] = _gameLoopState,
            // };
            // _states = states.ToDictionary(state => state.GetType());

            AddStatesActions();

            Enter<BootstrapState>();
        }

        private void AddStatesActions()
        {
            // ((BootstrapState)_bootstrapState).Entered += BootstrapStateEntered;
            // ((LoadLevelState)_loadLevelState).Entered += LoadLevelStateEntered;
            // ((LoadProgressState)_loadProgressState).Entered += LoadProgressStateEntered;
        }

        private void BootstrapStateEntered()
        {
            Enter<LoadProgressState>();
            // ((BootstrapState)_bootstrapState).Entered -= BootstrapStateEntered;
        }

        private void LoadLevelStateEntered()
        {
            Enter<GameLoopState>();
            // ((LoadLevelState)_loadLevelState).Entered -= LoadLevelStateEntered;
        }

        private void LoadProgressStateEntered()
        {
            Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
            // ((LoadProgressState)_loadProgressState).Entered -= LoadProgressStateEntered;
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