using CodeBase.Infrastructure.States;
using Zenject;

namespace CodeBase.Installers.SceneContext
{
    public class GameStateMachineInstaller : MonoInstaller
        // , IInitializable
    {
        public override void InstallBindings()
        {
            // BindInstallerInterfaces();
            BindGameStateMachine();
        }

        private void BindInstallerInterfaces()
        {
            Container.BindInterfacesTo<GameStateMachineInstaller>().FromInstance(this).AsSingle();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<GameStateMachine>()
                .FromNew()
                .AsSingle();
        }

        // public void Initialize()
        // {
            // GameStateMachine gameStateMachine = Container.Resolve<GameStateMachine>();
            // gameStateMachine.Enter<BootstrapState>();
        // }
    }
}