using CodeBase.Infrastructure.Factory;
using CodeBase.Services.Randomizer;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class LootSpawner : MonoBehaviour
        // , ISavedProgress
    {
        [SerializeField] private EnemyDeath _enemyDeath;

        private IGameFactory _factory;
        private int _lootMin;
        private int _lootMax;

        private IRandomService _random;

        private Loot _loot;

        public void Construct(IGameFactory factory, IRandomService random)
        {
            _factory = factory;
            _random = random;
        }

        private void Start()
        {
            _enemyDeath.Happened += SpawnLoot;
        }

        private void SpawnLoot()
        {
            LootPiece lootPiece = _factory.CreateLoot();
            lootPiece.transform.position = transform.position;

            _loot = GenerateLoot();

            lootPiece.Initialize(_loot);

            _enemyDeath.Happened -= SpawnLoot;
        }

        private Loot GenerateLoot()
        {
            return new Loot(_random.Next(_lootMin, _lootMax));
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
    }
}