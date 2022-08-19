using System.Collections.Generic;
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

        GameObject CreateHero(GameObject at);
        GameObject CreateHud();
        void CleanupCode();

        GameObject CreateMonster(MonsterTypeId typeId, Transform parent, string id);
        LootPiece CreateLoot();
    }
}