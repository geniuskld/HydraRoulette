using System;
using System.Security.Cryptography.X509Certificates;

namespace Md5Roulette.Logic
{
    public class Roulette
    {
        private readonly ISequenceGenerator _sequenceGenerator;

        /// <summary>
        /// For really random generation
        /// </summary>
        /// <param name="minNumber"></param>
        /// <param name="numbersCount"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Roulette(int minNumber, int numbersCount)
        {
            MinNumber = minNumber;
            NumbersCount = numbersCount;
            _sequenceGenerator = new RandomSequenceGenerator();
        }

        /// <summary>
        /// Determined
        /// </summary>
        /// <param name="rouletteNumber"></param>
        /// <param name="minNumber"></param>
        /// <param name="numbersCount"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Roulette(ulong rouletteNumber, int minNumber, int numbersCount)
        {
            if (rouletteNumber <= 0) throw new ArgumentOutOfRangeException(nameof(rouletteNumber));
            RouletteNumber = rouletteNumber;
            MinNumber = minNumber;
            NumbersCount = numbersCount;
            _sequenceGenerator = new DeterminedSequenceGenerator(rouletteNumber);
        }

        public Roulette(ISequenceGenerator sequenceGenerator, int minNumber, int numbersCount)
        {
            if (minNumber < 0) throw new ArgumentOutOfRangeException(nameof(minNumber));
            if (numbersCount <= 0) throw new ArgumentOutOfRangeException(nameof(numbersCount));
            MinNumber = minNumber;
            NumbersCount = numbersCount;
            _sequenceGenerator = sequenceGenerator ?? throw new ArgumentNullException(nameof(sequenceGenerator));
        }

        public ulong RouletteNumber { get; }
        public int MinNumber { get; }
        public int NumbersCount { get; }
        private int MaxNumber => MinNumber + NumbersCount;
        private string _sequence;

        public string GenerateSequence()
        {
            if (string.IsNullOrEmpty(_sequence))
                _sequence = _sequenceGenerator.GetSequence(MinNumber, MaxNumber);
            
            return _sequence;
        }

        /// <summary>
        /// Md5(rouletteNumber+Sequence)
        /// </summary>
        /// <returns></returns>
        public string GenerateMd5RouletteSequence()
        {
            GenerateSequence();
            return $"{RouletteNumber}.{_sequence}";
        }
    }
}