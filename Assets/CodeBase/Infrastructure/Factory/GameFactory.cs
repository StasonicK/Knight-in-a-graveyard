using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Randomizer;
using CodeBase.Services.SaveLoad;
using CodeBase.StaticData;
using CodeBase.UI;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressService;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private GameObject _heroGameObject { get; set; }

        public GameFactory(IAssetProvider assets, IStaticDataService staticData,
            IPersistentProgressService progressService)
        {
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
        }

        public GameObject CreateHero(GameObject at)
        {
            _heroGameObject = InstantiateRegistered(AssetPath.HeroPath, at.transform.position);
            return _heroGameObject;
        }

        public GameObject CreateHud()
        {
            GameObject hud = InstantiateRegistered(AssetPath.HudPath);
            hud.GetComponentInChildren<LootCounter>().Construct(_progressService.Progress.WorldData);
            return hud;
        }

        public GameObject CreateMonster(MonsterTypeId typeId, Transform parent, string id)
        {
            MonsterStaticData monsterData = _staticData.ForMonster(typeId);
            GameObject monster = Object.Instantiate(monsterData.Prefab, parent.position, Quaternion.identity, parent);

            IHealth health = monster.GetComponent<IHealth>();
            health.Current = monsterData.Hp;
            health.Max = monsterData.Hp;

            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

            LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.SetLoot(monsterData.MinLoot, monsterData.MaxLoot);
            lootSpawner.Construct(this, AllServices.Container.Single<IRandomService>(), id);

            Attack attack = monster.GetComponent<Attack>();
            attack.Construct(_heroGameObject.transform);
            attack.Damage = monsterData.Damage;
            attack.Cleavage = monsterData.Cleavage;
            attack.EffectiveDistance = monsterData.EffectiveDistance;

            monster.GetComponent<AgentMoveToHero>()?.Construct(_heroGameObject.transform);
            monster.GetComponent<RotateToHero>()?.Construct(_heroGameObject.transform);

            return monster;
        }

        public LootPiece CreateLoot()
        {
            var lootPiece = InstantiateRegistered(AssetPath.Loot).GetComponent<LootPiece>();
            lootPiece.Construct(_progressService.Progress.WorldData);
            return lootPiece;
        }

        public void CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId)
        {
            SpawnPoint spawner = InstantiateRegistered(AssetPath.Spawner, at)
                .GetComponent<SpawnPoint>();

            spawner.Construct(this);
            spawner.Id = spawnerId;
            spawner.MonsterTypeId = monsterTypeId;
        }

        public void CleanupCode()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 position)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath, at: position);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }
    }
}