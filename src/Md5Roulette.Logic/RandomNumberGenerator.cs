using System;

namespace Md5Roulette.Logic
{
    internal class RandomSequenceGenerator : ISequenceGenerator
    {
        /// <inheritdoc />
        public string GetSequence(int minNumber, int maxNumber)
        {
            var rnd = new Random(DateTime.UtcNow.Ticks.GetHashCode());
            return $"{NextFloat(rnd,minNumber,maxNumber)}";
        }

        public static float NextFloat(Random random, int minNumber, int maxNumber)
        {
            var result = random.NextDouble() * (maxNumber - minNumber) + minNumber;
            return (float) result;
        }
    }
}