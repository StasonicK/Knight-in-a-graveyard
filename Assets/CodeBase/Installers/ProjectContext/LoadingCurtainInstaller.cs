using CodeBase.Logic;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers.ProjectContext
{
    public class LoadingCurtainInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;

        public override void InstallBindings()
        {
            BindLoadingCurtain();
        }

        private void BindLoadingCurtain()
        {
            Container
                .Bind<ILoadingCurtain>()
                .To<LoadingCurtain>()
                .FromNewComponentOnNewPrefab(_loadingCurtain)
                .AsSingle();
        }
    }
}