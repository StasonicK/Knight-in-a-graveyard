using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class SpawnerCreator
    {
        private IAssets _assets;

        public SpawnerCreator(IAssets assets)
        {
            _assets = assets;
        }

        public async Task CreateSpawner(string spawnerId, Vector3 at, MonsterTypeId monsterTypeId)
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAddress.Spawner);
            SpawnPoint spawner = _assets.InstantiateRegistered(prefab, at)
                .GetComponent<SpawnPoint>();
            spawner.Construct(this);
            spawner.Id = spawnerId;
            spawner.MonsterTypeId = monsterTypeId;
        }
    }
}