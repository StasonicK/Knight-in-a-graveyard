using CodeBase.Services.Ads;
using Zenject;

namespace CodeBase.Installers.SceneContext
{
    public class AdsServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAdsService>()
                .To<AdsService>()
                .FromNew()
                .AsSingle()
                // .NonLazy()
                ;
        }
    }
}