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

        // private string _id;
        private Loot _loot;

        public void Construct(IGameFactory factory, IRandomService random
            // , string id
        )
        {
            // _id = id;
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

            _loot = GenerateLoot(
                // _id
            );

            lootPiece.Initialize(_loot);

            _enemyDeath.Happened -= SpawnLoot;
        }

        private Loot GenerateLoot(
            // string id
        )
        {
            return new Loot(_random.Next(_lootMin, _lootMax)
                // , id
            );
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }

        // public void LoadProgress(PlayerProgress progress)
        // {
        //     for (int i = 0; i < progress.KillData.DropedLoots.Count; i++)
        //     {
        //         if (progress.KillData.DropedLoots[i].Loot.Id == _id)
        //         {
        //             RestoreLoot(progress, i);
        //             progress.KillData.DropedLoots.RemoveAt(i);
        //             return;
        //         }
        //     }
        // }
        //
        // private void RestoreLoot(PlayerProgress progress, int i)
        // {
        //     DropedLoot dropedLoot = progress.KillData.DropedLoots[i];
        //     _loot = dropedLoot.Loot;
        //     LootPiece lootPiece = _factory.CreateLoot();
        //     lootPiece.transform.position = dropedLoot.PositionOnLevel.AsUnityVector();
        //     lootPiece.Initialize(_loot);
        // }
        //
        // public void UpdateProgress(PlayerProgress progress)
        // {
        //     progress.KillData.DropedLoots.Add(new DropedLoot(transform.position.AsVectorData(), _loot));
        // }
    }
}