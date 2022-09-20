using CodeBase.Infrastructure.AssetManagement;
using Zenject;

namespace CodeBase.Installers.SceneContext
{
    public class AssetsProviderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAssets>()
                .To<AssetsProvider>()
                .FromNew()
                .AsSingle()
                // .NonLazy()
                ;
        }
    }
}