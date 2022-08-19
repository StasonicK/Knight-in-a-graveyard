using System;
using CodeBase.Enemy;

namespace CodeBase.Data
{
    [Serializable]
    public class DropedLoot
    {
        public Vector3Data PositionOnLevel { get; private set; }
        public Loot Loot { get; private set; }

        public DropedLoot(Vector3Data positionOnLevel, Loot loot)
        {
            PositionOnLevel = positionOnLevel;
            Loot = loot;
        }
    }
}