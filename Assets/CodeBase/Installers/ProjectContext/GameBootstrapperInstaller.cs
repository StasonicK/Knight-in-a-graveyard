using CodeBase.Infrastructure;
using CodeBase.Logic;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers.ProjectContext
{
    public class GameBootstrapperInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;
        [SerializeField] private GameBootstrapper _gameBootstrapper;

        public override void InstallBindings()
        {
            BindLoadingCurtain();
            BindSceneLoader();
            BindGame();
            BindGameBootstrapper();
        }

        private void BindLoadingCurtain()
        {
            Container
                .Bind<ILoadingCurtain>()
                .To<LoadingCurtain>()
                .FromNewComponentOnNewPrefab(_loadingCurtain)
                .AsSingle()
                .NonLazy();
        }

        private void BindSceneLoader()
        {
            Container.Bind<ISceneLoader>()
                .To<SceneLoader>()
                .FromInstance(new SceneLoader(_gameBootstrapper))
                .AsSingle()
                .NonLazy();
        }

        private void BindGame()
        {
            Container
                .Bind<IGame>()
                .To<Game>()
                .FromNewComponentOnNewGameObject()
                .AsSingle()
                .NonLazy();
        }

        private void BindGameBootstrapper()
        {
            Container
                .Bind<ICoroutineRunner>()
                .To<GameBootstrapper>()
                .FromComponentInNewPrefab(_gameBootstrapper)
                .AsSingle()
                .NonLazy();
        }
    }
}