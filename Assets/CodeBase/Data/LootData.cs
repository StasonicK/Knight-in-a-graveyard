using System;
using CodeBase.Enemy;

namespace CodeBase.Data
{
    [Serializable]
    public class LootData
    {
        public int Collected;
        public LootPieceDataDictionary LootPieceDataOnScene = new LootPieceDataDictionary();

        public Action Changed;

        public void Collect(Loot loot)
        {
            Collected += loot.Value;
            Changed?.Invoke();
        }

        public void Add(int loot)
        {
            Collected += loot;
            Changed?.Invoke();
        }
    }
}