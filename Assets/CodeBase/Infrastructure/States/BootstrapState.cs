using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";

        [Inject] private readonly IGameStateMachine _stateMachine;

        // [Inject]   
        private readonly ISceneLoader _sceneLoader;
        // private AllServices _services;

        [Inject]
        public BootstrapState(
            // IGameStateMachine stateMachine,
            ISceneLoader sceneLoader
        // , AllServices services
        )
        {
            // _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            // _services = services;

            // RegisterServices();
        }

        public void Enter() => _sceneLoader.Load(name: Initial, onLoaded: EnterLoadLevel);

        private void EnterLoadLevel() => _stateMachine.Enter<LoadProgressState>();

        // private void RegisterServices()
        // {
        //     RegisterStaticData();
        //     RegisterAdsService();
        //
        //     _services.RegisterSingle<IGameStateMachine>(_stateMachine);
        //     RegisterAssetsProvider();
        //     _services.RegisterSingle<IInputService>(InputService());
        //     _services.RegisterSingle<IRandomService>(new RandomService());
        //     _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
        //
        //     RegisterIAPService(new IAPProvider(), _services.Single<IPersistentProgressService>());
        //
        //     _services.RegisterSingle<IUIFactory>(
        //         new UIFactory(_services.Single<IAssets>(),
        //             _services.Single<IStaticDataService>(),
        //             _services.Single<IPersistentProgressService>(),
        //             _services.Single<IAdsService>(),
        //             _services.Single<IIAPService>())
        //     );
        //     _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));
        //
        //     _services.RegisterSingle<IGameFactory>(
        //         new GameFactory(
        //             _services.Single<IAssets>(),
        //             _services.Single<IStaticDataService>(),
        //             _services.Single<IRandomService>(),
        //             _services.Single<IPersistentProgressService>(),
        //             _services.Single<IWindowService>(),
        //             _services.Single<IGameStateMachine>()
        //         ));
        //
        //     _services.RegisterSingle<ISaveLoadService>(
        //         new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
        // }
        //
        // private void RegisterStaticData()
        // {
        //     IStaticDataService staticData = new StaticDataService();
        //     staticData.Load();
        //     _services.RegisterSingle(staticData);
        // }
        //
        // private void RegisterAdsService()
        // {
        //     var adsService = new AdsService();
        //     adsService.Initialize();
        //     _services.RegisterSingle<IAdsService>(adsService);
        // }
        //
        // private void RegisterIAPService(IAPProvider iapProvider, IPersistentProgressService progressService)
        // {
        //     IAPService iapService = new IAPService(iapProvider, progressService);
        //     iapService.Initialize();
        //     _services.RegisterSingle<IIAPService>(iapService);
        // }
        //
        // private void RegisterAssetsProvider()
        // {
        //     var assetsProvider = new AssetsProvider();
        //     assetsProvider.Initialize();
        //     _services.RegisterSingle<IAssets>(assetsProvider);
        // }

        public void Exit()
        {
        }

        // private static IInputService InputService()
        // {
        //     if (Application.isEditor)
        //         return new StandaloneInputService();
        //     else
        //         return new MobileInputService();
        // }
    }
}