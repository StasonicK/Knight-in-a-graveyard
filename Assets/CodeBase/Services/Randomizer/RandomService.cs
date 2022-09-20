using UnityEngine;

namespace CodeBase.Services.Randomizer
{
    public sealed class RandomService : IRandomService
    {
        public int Next(int min, int max) =>
            Random.Range(min, max);
    }
}