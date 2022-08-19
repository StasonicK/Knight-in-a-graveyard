using System;

namespace CodeBase.Enemy
{
    [Serializable]
    public class Loot
    {
        public string Id { get; private set; }
        
        public int Value { get; private set; }

        public Loot(int value, string id)
        {
            Value = value;
            Id = id;
        }
    }
}