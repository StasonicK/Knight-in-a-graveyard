using CodeBase.Services.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers.SceneContext
{
    public class InputServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputService>()
                .FromInstance(InputService())
                .AsSingle()
                // .NonLazy()
                ;
        }

        private static IInputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }
    }
}