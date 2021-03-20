using System;
using System.Security.Cryptography;
using System.Text;
using Md5Roulette.Logic.NumberGenerators;
using RandomNumberGenerator = Md5Roulette.Logic.NumberGenerators.RandomNumberGenerator;

namespace Md5Roulette.Logic
{
    public class Roulette
    {
        private readonly INumberGenerator _numberGenerator = new RandomNumberGenerator();
        private readonly HashAlgorithm _hashAlgorithm = MD5.Create();

        public Roulette(ulong rouletteNumber, int minNumber, int numbersCount, INumberGenerator numberGenerator =
            default, HashAlgorithm hashAlgorithm = default)
        {
            if (minNumber < 0) throw new ArgumentOutOfRangeException(nameof(minNumber));
            if (numbersCount <= 0) throw new ArgumentOutOfRangeException(nameof(numbersCount));
         
            RouletteNumber = rouletteNumber;
            MinNumber = minNumber;
            NumbersCount = numbersCount;

            if (numberGenerator != default)
                _numberGenerator = numberGenerator;
            if (hashAlgorithm != default)
                _hashAlgorithm = hashAlgorithm;
        }

        public ulong RouletteNumber { get; }
        public int MinNumber { get; }
        public int NumbersCount { get; }
        private int MaxNumber => MinNumber + NumbersCount;
        public float? Number { get; private set; }


        /// <summary>
        /// Returns {rouletteNumber.GeneratedFloatNumber}
        /// </summary>
        /// <returns></returns>
        public string GenerateSequence()
        {
            Number ??= _numberGenerator.GetNumber(MinNumber, MaxNumber);
            return $"{RouletteNumber}.{Number}";
        }

        /// <summary>
        /// Md5(rouletteNumber+Sequence)
        /// </summary>
        /// <returns></returns>
        public string GenerateRouletteHash()
        {
            var byteToHash = Encoding.UTF8.GetBytes(GenerateSequence());
            var hashedBytes = _hashAlgorithm.ComputeHash(byteToHash);

            // Convert the byte array to hexadecimal string
            var sb = new StringBuilder();
            foreach (var t in hashedBytes) sb.Append(t.ToString("X2"));
            return sb.ToString();
        }
    }
}