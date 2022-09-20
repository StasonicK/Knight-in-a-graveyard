using CodeBase.Services.SaveLoad;
using Zenject;

namespace CodeBase.Installers.SceneContext
{
    public class SaveLoadServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISaveLoadService>()
                .To<SaveLoadService>()
                .FromNew()
                .AsSingle()
                // .NonLazy()
                ;
        }
    }
}