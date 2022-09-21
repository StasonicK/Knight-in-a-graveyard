using CodeBase.Logic;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class Game : MonoBehaviour, IGame
    {
        public ILoadingCurtain LoadingCurtain;

        // [Inject] 
        // private   IGameStateMachine _gameStateMachine;

        // [Inject] 
        private ISceneLoader _sceneLoader;
        // [Inject] private IGameFactory _gameFactory;
        // [Inject] private IPersistentProgressService _progressService;
        // [Inject] private IStaticDataService _staticDataService;
        // [Inject] private IUIFactory _uiFactory;
        // [Inject] private ISaveLoadService _saveLoadService;

        [Inject]
        public Game(
            ISceneLoader sceneLoader,
            ILoadingCurtain loadingCurtain)
        {
            LoadingCurtain = loadingCurtain;
            // GameStateMachine = new GameStateMachine(
            //     // sceneLoader, 
            //     LoadingCurtain
            //     // , _gameFactory, _progressService, _staticDataService, _uiFactory,
            //     // _saveLoadService
            //     );
        }
        //
        // public IGameStateMachine GetGameStateMachine()
        // {
        //     return _gameStateMachine;
        // }
    }
}