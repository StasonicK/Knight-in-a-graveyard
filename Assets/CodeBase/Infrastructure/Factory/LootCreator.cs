using System.Threading.Tasks;
using CodeBase.Enemy;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Windows;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Factory
{
    public class LootCreator
    {
        private readonly AssetsCreator _assetsCreator;
        private readonly IAssets _assets;
        private readonly IWindowService _windowService;
        private readonly IPersistentProgressService _persistentProgressService;

        [Inject]
        public LootCreator(AssetsCreator assetsCreator, IWindowService windowService, IPersistentProgressService persistentProgressService)
        {
            _assetsCreator = assetsCreator;
            _windowService = windowService;
            _persistentProgressService = persistentProgressService;
        }
        

        public async Task<LootPiece> CreateLoot()
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAddress.Loot);
            LootPiece lootPiece = _assets.InstantiateRegistered(prefab).GetComponent<LootPiece>();
            lootPiece.Construct(_persistentProgressService.Progress.WorldData);
            return lootPiece;
        }
        
        public void Register(LootPiece lootPiece)
        {
        }

        public void Unregister(LootPiece lootPiece)
        {
        }
    }
}