using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers.ProjectContext
{
    public class GameStateMachineInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            BindInstallerInterfaces();
            BindGameStateMachine();
        }

        private void BindInstallerInterfaces()
        {
            Container.BindInterfacesTo<GameStateMachineInstaller>().FromInstance(this).AsSingle();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<IGameStateMachine>()
                .To<GameStateMachine>()
                .FromInstance(new GameStateMachine())
                .AsSingle();
        }

        public void Initialize()
        {
            IGameStateMachine gameStateMachine = Container.Resolve<IGameStateMachine>();
            gameStateMachine.Enter<BootstrapState>();
        }
    }
}