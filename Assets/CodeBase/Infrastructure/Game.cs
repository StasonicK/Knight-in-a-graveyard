using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class Game : MonoBehaviour
    {
        public ILoadingCurtain LoadingCurtain;

        // [Inject] 
        public readonly IGameStateMachine GameStateMachine;

        // [Inject] private ISceneLoader _sceneLoader;
        // [Inject] private IGameFactory _gameFactory;
        // [Inject] private IPersistentProgressService _progressService;
        // [Inject] private IStaticDataService _staticDataService;
        // [Inject] private IUIFactory _uiFactory;
        // [Inject] private ISaveLoadService _saveLoadService;

        public Game(ISceneLoader sceneLoader, ILoadingCurtain loadingCurtain)
        {
            LoadingCurtain = loadingCurtain;
            GameStateMachine = new GameStateMachine(
                sceneLoader, 
                LoadingCurtain
                // , _gameFactory, _progressService, _staticDataService, _uiFactory,
                // _saveLoadService
                );
        }
    }
}