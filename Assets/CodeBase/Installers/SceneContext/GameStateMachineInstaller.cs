using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers.SceneContext
{
    public class GameStateMachineInstaller : MonoInstaller
    {
        [Inject] private ISceneLoader _sceneLoader;
        [Inject] private ILoadingCurtain _loadingCurtain;

        public override void InstallBindings()
        {
            BindGameStateMachine();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<IGameStateMachine>()
                .To<GameStateMachine>()
                .FromInstance(new GameStateMachine(_sceneLoader, _loadingCurtain))
                .AsSingle()
                .NonLazy();
        }
    }
}