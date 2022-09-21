using CodeBase.UI.Services.Windows;
using Zenject;

namespace CodeBase.Installers.SceneContext
{
    public class WindowServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IWindowService>()
                .To<WindowService>()
                .FromNew()
                .AsSingle()
                // .NonLazy()
                ;
        }
    }
}