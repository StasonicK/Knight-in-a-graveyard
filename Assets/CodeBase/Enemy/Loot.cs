using System;

namespace CodeBase.Enemy
{
    [Serializable]
    public class Loot
    {
        public int Value { get; private set; }

        public Loot(int value)
        {
            Value = value;
        }
    }
}