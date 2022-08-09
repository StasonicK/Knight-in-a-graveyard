using UnityEngine;

public class BootstrapState : IState
{
    private const string Initial = "Initial";

    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private AllServices _services;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _services = services;
    }

    public void Enter()
    {
        RegisterServices();
        _sceneLoader.Load(name: Initial, onLoaded: EnterLoadLevel);
    }

    private void EnterLoadLevel() => _stateMachine.Enter<LoadLevelState, string>("Main");

    private void RegisterServices()
    {
        _services.RegisterSingle<IInputService>(InputService());
        _services.RegisterSingle<IAssetProvider>(new AssetProvider());
        _services.RegisterSingle<IGameFactory>(
            new GameFactory(_services.Single<IAssetProvider>()));
    }

    public void Exit()
    {
    }

    private static IInputService InputService()
    {
        if (Application.isEditor)
            return new StandaloneInputService();
        else
            return new MobileInputService();
    }
}