using CodeBase.Services.StaticData;
using Zenject;

namespace CodeBase.Installers.SceneContext
{
    public class StaticDataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IStaticDataService>()
                .To<StaticDataService>()
                .FromNew()
                .AsSingle()
                // .NonLazy()
                ;
        }
    }
}