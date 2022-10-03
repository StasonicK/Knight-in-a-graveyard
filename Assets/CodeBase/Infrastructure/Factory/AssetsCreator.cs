using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Factory
{
    public class AssetsCreator
    {
        private readonly IAssets _assets;


        [Inject]
        public AssetsCreator(IAssets assets)
        {
            _assets = assets;
        }

    }
}