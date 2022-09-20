using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers.ProjectContext
{
    public class GameBootstrapperInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private GameBootstrapper _gameBootstrapper;

        public override void InstallBindings()
        {
            BindInstallerInterfaces();
            BindGameBootstrapper();
        }

        private void BindInstallerInterfaces()
        {
            Container.BindInterfacesTo<GameBootstrapperInstaller>().FromInstance(this).AsSingle();
        }

        private void BindGameBootstrapper()
        {
            Container
                .Bind<GameBootstrapper>()
                .FromNewComponentOnNewPrefab(_gameBootstrapper)
                .AsSingle();
        }

        public void Initialize()
        {
            IGameStateMachine gameStateMachine = Container.Resolve<IGameStateMachine>();
            gameStateMachine.Enter<BootstrapState>();
        }
    }
}