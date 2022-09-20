using CodeBase.Infrastructure;
using CodeBase.Logic;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers.ProjectContext
{
    public class GameInstaller : MonoInstaller
    {
        [Inject] private ILoadingCurtain _loadingCurtain;
        [Inject] private ISceneLoader _sceneLoader;

        public override void InstallBindings()
        {
            BindGame();
        }

        private void BindGame()
        {
            Container
                .Bind<Game>()
                .FromInstance(new Game(_sceneLoader, _loadingCurtain))
                .AsSingle();
        }
    }
}