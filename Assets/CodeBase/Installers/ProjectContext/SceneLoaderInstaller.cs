using CodeBase.Infrastructure;
using Zenject;

namespace CodeBase.Installers.ProjectContext
{
    public class SceneLoaderInstaller : MonoInstaller
    {
        [Inject] private GameBootstrapper _gameBootstrapper;

        public override void InstallBindings()
        {
            BindSceneLoader();
        }

        private void BindSceneLoader()
        {
            Container.Bind<ISceneLoader>()
                .To<SceneLoader>()
                .FromInstance(new SceneLoader(_gameBootstrapper))
                .AsSingle()
                .NonLazy();
        }
    }
}