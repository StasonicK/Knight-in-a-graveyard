using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        [SerializeField] public MonsterTypeId MonsterTypeId;
        public string Id { get; set; }

        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;
        private bool _slain;

        public void Construct(IGameFactory factory) =>
            _factory = factory;

        public void LoadProgress(PlayerProgress progress)
        {
            if (!progress.KillData.ClearedSpawners.Contains(Id))
                Spawn();
        }

        private void Spawn() =>
            _factory.CreateMonster(MonsterTypeId, transform, Id)
                .GetComponent<EnemyDeath>()
                .Happened += Slain;

        private void Slain() =>
            _slain = true;

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_slain)
                progress.KillData.ClearedSpawners.Add(Id);
        }
    }
}