using CodeBase.Logic;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers.SceneContext
{
    public class LoadingCurtainInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;

        public override void InstallBindings()
        {
            Container.Bind<ILoadingCurtain>()
                .To<LoadingCurtain>()
                .FromInstance(_loadingCurtain)
                .AsSingle()
                // .NonLazy()
                ;
        }
    }
}