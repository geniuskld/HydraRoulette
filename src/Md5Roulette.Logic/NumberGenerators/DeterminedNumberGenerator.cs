using System;

namespace Md5Roulette.Logic.NumberGenerators
{
    public class DeterminedNumberGenerator : INumberGenerator
    {
        private readonly int _rouletteNumber;

        public DeterminedNumberGenerator(ulong rouletteNumber)
        {
            _rouletteNumber = rouletteNumber.GetHashCode();
        }

        /// <inheritdoc />
        /// <inheritdoc />
        public float GetNumber(int minNumber, int maxNumber)
        {
            var rnd = new Random(_rouletteNumber);
            return RandomNumberGenerator.NextFloat(rnd, minNumber, maxNumber);
        }
    }
}