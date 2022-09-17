using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Enemy;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        Task WarmUp();
        Task<GameObject> CreateHero(Vector3 at);

        Task<GameObject> CreateMonster(MonsterTypeId typeId, Transform parent);

        Task<GameObject> CreateHud();
        Task<LootPiece> CreateLoot();
        Task CreateSpawner(string spawnerId, Vector3 at, MonsterTypeId spawnerDataMonsterTypeId);
        void CleanUp();
        void Register(LootPiece lootPiece);
        void Unregister(LootPiece lootPiece);
        Task CreateLevelTransfer(Vector3 at);
    }
}