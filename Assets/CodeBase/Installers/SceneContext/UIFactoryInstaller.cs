using CodeBase.UI.Services.Factory;
using Zenject;

namespace CodeBase.Installers.SceneContext
{
    public class UIFactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IUIFactory>()
                .To<UIFactory>()
                .FromNew()
                .AsSingle()
                // .NonLazy()
                ;
        }
    }
}