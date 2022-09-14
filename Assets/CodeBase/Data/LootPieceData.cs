using System;
using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.Data
{
    [Serializable]
    public class LootPieceData
    {
        public Vector3 Position;
        public Loot Loot;

        public LootPieceData(Vector3 position, Loot loot)
        {
            Position = position;
            Loot = loot;
        }
    }
}