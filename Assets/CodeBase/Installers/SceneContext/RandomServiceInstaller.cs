using CodeBase.Services.Randomizer;
using Zenject;

namespace CodeBase.Installers.SceneContext
{
    public class RandomServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IRandomService>()
                .To<RandomService>()
                .FromNew()
                .AsSingle()
                // .NonLazy()
                ;
        }
    }
}