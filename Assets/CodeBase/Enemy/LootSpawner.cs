using CodeBase.Infrastructure.Factory;
using CodeBase.Services.Randomizer;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;

        private GameFactory _factory;
        private int _lootMin;
        private int _lootMax;

        private IRandomService _random;

        private Loot _loot;

        public void Construct(GameFactory factory, IRandomService random)
        {
            _factory = factory;
            _random = random;
        }

        private void Start()
        {
            _enemyDeath.Died += SpawnLoot;
        }

        private async void SpawnLoot()
        {
            LootPiece lootPiece = await _factory.CreateLoot();
            lootPiece.transform.position = transform.position;

            _loot = GenerateLoot();

            lootPiece.Initialize(_loot);
            _factory.Register(lootPiece);
            lootPiece.Picked += () => _factory.Unregister(lootPiece);
        }

        private Loot GenerateLoot()
        {
            return new Loot { Value = _random.Next(_lootMin, _lootMax) };
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
    }
}