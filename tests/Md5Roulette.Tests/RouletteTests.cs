using System;
using FluentAssertions;
using Md5Roulette.Logic;
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
        [InlineData(0, 100)]
        [InlineData(20, 300)]
        public void RandomRouletteTest(int minValue, int maxValue)
        {
            var sut = new Roulette(minValue, maxValue);
            var sequence1 = sut.GenerateSequence();

            sut = new Roulette(minValue, maxValue);
            var sequence2 = sut.GenerateSequence();

            _testOutputHelper.WriteLine(sequence2);
            sequence1.Should().NotBe(sequence2, $"{sequence1}=={sequence2} is not rally random");
        }

        [Theory]
        [InlineData(1, 0, 100)]
        [InlineData(2, 20, 300)]
        public void DeterminedRouletteTest(ulong rouletteNumber, int minValue, int maxValue)
        {
            var sut = new Roulette(rouletteNumber, minValue, maxValue);
            var sequence1 = sut.GenerateSequence();

            sut = new Roulette(rouletteNumber, minValue, maxValue);
            var sequence2 = sut.GenerateSequence();

            sequence1.Should().Be(sequence2, $"{sequence1}!={sequence2}");
        }
    }
}