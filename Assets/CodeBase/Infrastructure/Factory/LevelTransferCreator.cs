using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Factory
{
    public class LevelTransferCreator
    {
        private readonly IAssets _assets;
        private readonly GameStateMachine _gameStateMachine;
        
        [Inject]
        public LevelTransferCreator(IAssets assets)
        {
            _assets = assets;
        }

        public async Task CreateLevelTransfer(Vector3 at)
        {
            GameObject prefab = await _assets.InstantiateRegisteredAsync(AssetAddress.LevelTransferTrigger, at);
            LevelTransferTrigger levelTransfer = prefab.GetComponent<LevelTransferTrigger>();

            levelTransfer.Construct(_gameStateMachine);
        }
    }
}