using CodeBase.Services.IAP;
using Zenject;

namespace CodeBase.Installers.SceneContext
{
    public class IAPServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IIAPService>()
                .To<IAPService>()
                .FromNew()
                .AsSingle()
                // .NonLazy()
                ;
        }
    }
}