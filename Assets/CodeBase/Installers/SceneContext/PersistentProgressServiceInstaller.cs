using CodeBase.Services.PersistentProgress;
using Zenject;

namespace CodeBase.Installers.SceneContext
{
    public class PersistentProgressServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IPersistentProgressService>()
                .To<PersistentProgressService>()
                .FromNew()
                .AsSingle()
                // .NonLazy()
                ;
        }
    }
}