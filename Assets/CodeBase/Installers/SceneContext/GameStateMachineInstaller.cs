using CodeBase.Infrastructure.States;
using Zenject;

namespace CodeBase.Installers.SceneContext
{
    public class GameStateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Container.BindInterfacesTo<GameStateMachineInstaller>().AsSingle();
            BindGameStateMachine();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<GameStateMachine>()
                .FromNew()
                .AsSingle()
                .NonLazy()
                ;
        }
    }
}