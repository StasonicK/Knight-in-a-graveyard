using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public LoadingCurtain CurtainPrefab;
        public ILoadingCurtain LoadingCurtain;
        public ISceneLoader SceneLoader;
        private Game _game;

        private void Awake()
        {
            // LoadingCurtain = Instantiate(CurtainPrefab);
            // SceneLoader = new SceneLoader(this);
            // _game = new Game(SceneLoader, LoadingCurtain);
            // _game.GameStateMachine.Enter<BootstrapState>();
            //
            // DontDestroyOnLoad(this);
        }
    }
}