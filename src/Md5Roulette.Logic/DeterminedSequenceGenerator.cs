using System;

namespace Md5Roulette.Logic
{
    internal class DeterminedSequenceGenerator : ISequenceGenerator
    {
        private readonly int _rouletteNumber;

        public DeterminedSequenceGenerator(ulong rouletteNumber)
        {
            _rouletteNumber = rouletteNumber.GetHashCode();
        }

        /// <inheritdoc />
        public string GetSequence(int minNumber, int maxNumber)
        {
            var rnd = new Random(_rouletteNumber);
            return $"{RandomSequenceGenerator.NextFloat(rnd, minNumber, maxNumber):F13}";
        }
    }
}