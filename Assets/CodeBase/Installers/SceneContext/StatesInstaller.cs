using CodeBase.Infrastructure.States;
using Zenject;

namespace CodeBase.Installers.SceneContext
{
    public class StatesInstaller : MonoInstaller
    {
        // [Inject] private ILoadingCurtain _loadingCurtain;
        // [Inject] private ISceneLoader _sceneLoader;
        // [Inject] private IGameFactory _gameFactory;
        // [Inject] private IPersistentProgressService _progressService;
        // [Inject] private IStaticDataService _staticDataService;
        // [Inject] private IUIFactory _uiFactory;
        // [Inject] private ISaveLoadService _saveLoadService;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BootstrapState>().AsSingle();
            Container.BindInterfacesTo<LoadLevelState>().AsSingle();
            Container.BindInterfacesTo<LoadProgressState>().AsSingle();
            Container.BindInterfacesTo<GameLoopState>().AsSingle();

            // BindBootstrapState();
            // BindLoadLevelState();
            // BindLoadProgressState();
            // BindGameLoopState();
        }

        private void BindBootstrapState()
        {
            Container
                .Bind<IExitableState>()
                .To<BootstrapState>()
                .FromNew()
                .AsSingle()
                .WithConcreteId(States.BootstrapState);
        }

        private void BindLoadLevelState()
        {
            Container
                .Bind<IExitableState>()
                .To<LoadLevelState>()
                .FromNew()
                .AsSingle()
                .WithConcreteId(States.BootstrapState);
        }

        private void BindLoadProgressState()
        {
            Container
                .Bind<IExitableState>()
                .To<LoadProgressState>()
                .FromNew()
                .AsSingle()
                .WithConcreteId(States.BootstrapState);
        }

        private void BindGameLoopState()
        {
            Container
                .Bind<IExitableState>()
                .To<GameLoopState>()
                .FromNew()
                .AsSingle()
                .WithConcreteId(States.BootstrapState);
        }
    }
}