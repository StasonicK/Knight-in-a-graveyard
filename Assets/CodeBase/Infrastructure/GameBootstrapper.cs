using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public LoadingCurtain CurtainPrefab;

        private IGame _game;
        public ILoadingCurtain LoadingCurtain;

        public ISceneLoader SceneLoader;
        // private Game _game;

        private void Start()
        {
            // LoadingCurtain = Instantiate(CurtainPrefab);
            // SceneLoader = new SceneLoader(this);
            // _game = new Game(SceneLoader, LoadingCurtain);
            //
            // DontDestroyOnLoad(this);
        }

        [Inject]
        public void Construct(ILoadingCurtain loadingCurtain, ISceneLoader sceneLoader, IGame game)
        {
            LoadingCurtain = loadingCurtain;
            SceneLoader = sceneLoader;
            _game = game;

            _game.GetGameStateMachine().Enter<BootstrapState>();
        }
    }
}