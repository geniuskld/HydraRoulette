using System;
using FluentAssertions;
using Md5Roulette.Logic;
using Md5Roulette.Logic.NumberGenerators;
using Xunit;
using Xunit.Abstractions;

namespace Md5Roulette.Tests
{
    public class RouletteTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public RouletteTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [InlineData(1, 0, 100)]
        [InlineData(2, 20, 300)]
        public void RandomRouletteTest(ulong rouletteNumber, int minValue, int numbersCount)
        {
            var sut = new Roulette(rouletteNumber, minValue, numbersCount);
            var sequence1 = sut.GenerateSequence();

            sut.Number.Should().BeGreaterOrEqualTo(minValue);
            sut.Number.Should().BeLessOrEqualTo(minValue + numbersCount);

            sut = new Roulette(rouletteNumber, minValue, numbersCount);
            var sequence2 = sut.GenerateSequence();

            _testOutputHelper.WriteLine($"{sequence2} hash:{sut.GenerateRouletteHash()}");
            sequence1.Should().NotBe(sequence2, $"{sequence1}=={sequence2} is not rally random");
            sut.Number.Should().BeGreaterOrEqualTo(minValue);
            sut.Number.Should().BeLessOrEqualTo(minValue + numbersCount);
        }

        [Theory]
        [InlineData(1, 0, 100)]
        [InlineData(2, 20, 300)]
        public void DeterminedRouletteTest(ulong rouletteNumber, int minValue, int numbersCount)
        {
            var numGen = new DeterminedNumberGenerator(rouletteNumber);
            var sut = new Roulette(rouletteNumber, minValue, numbersCount, numGen);
            var sequence1 = sut.GenerateSequence();


            sut.Number.Should().BeGreaterOrEqualTo(minValue);
            sut.Number.Should().BeLessOrEqualTo(minValue + numbersCount);

            sut = new Roulette(rouletteNumber, minValue, numbersCount, numGen);
            var sequence2 = sut.GenerateSequence();

            sequence1.Should().Be(sequence2, $"{sequence1}!={sequence2}");

            _testOutputHelper.WriteLine($"{sequence2} hash:{sut.GenerateRouletteHash()}");
            sut.Number.Should().BeGreaterOrEqualTo(minValue);
            sut.Number.Should().BeLessOrEqualTo(minValue + numbersCount);
        }
    }
}