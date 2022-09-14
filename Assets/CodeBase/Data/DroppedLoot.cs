using System;
using CodeBase.Enemy;

namespace CodeBase.Data
{
    [Serializable]
    public class DroppedLoot
    {
        public Vector3Data PositionOnLevel { get; private set; }
        public Loot Loot { get; private set; }

        public DroppedLoot(Vector3Data positionOnLevel, Loot loot)
        {
            PositionOnLevel = positionOnLevel;
            Loot = loot;
        }
    }
}