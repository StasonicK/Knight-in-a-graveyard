using CodeBase.Infrastructure.Factory;
using Zenject;

namespace CodeBase.Installers.SceneContext
{
    public class GameFactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameFactory>()
                .To<GameFactory>()
                .FromNew()
                .AsSingle()
                // .NonLazy()
                ;
        }
    }
}