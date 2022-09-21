using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public LoadingCurtain CurtainPrefab;

        private IGame _game;
        public ILoadingCurtain LoadingCurtain;

        public ISceneLoader SceneLoader;
        // private Game _game;

        // [Inject] private IGameStateMachine _gameStateMachine;

        private void Start()
        {
            // LoadingCurtain = Instantiate(CurtainPrefab);
            // SceneLoader = new SceneLoader(this);
            // _game = new Game(SceneLoader, LoadingCurtain);
            //
            // DontDestroyOnLoad(this);
            // _gameStateMachine.Enter<BootstrapState>();
        }

        // [Inject]
        public void Construct(ILoadingCurtain loadingCurtain, ISceneLoader sceneLoader, IGame game)
        {
            LoadingCurtain = loadingCurtain;
            SceneLoader = sceneLoader;
            _game = game;

            // _game.GetGameStateMachine().Enter<BootstrapState>();
        }
    }
}