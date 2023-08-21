using BettingOddsCalculator.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingOddsCalculatorTests
{
    public class CalculationsTests
    {
        private readonly Mock<ILogger<Calculations>> _mockLogger;
        private readonly Calculations _calculations;

        public CalculationsTests()
        {
            _mockLogger = new Mock<ILogger<Calculations>>();
            _calculations = new Calculations(_mockLogger.Object);
        }

        [Theory]
        [InlineData(100, 100, 100, 200)]
        [InlineData(200, 100, 200, 300)]
        [InlineData(300, 50, 150, 200)]
        public void CalculateWinningsAndPayout_PositiveOdds_ReturnsCorrectResult(int americanOdds, int betAmount, double expectedWinnings, double expectedTotalPayout)
        {
            // Act
            var result = _calculations.CalculateWinningsAndPayout(americanOdds, betAmount);

            // Assert
            result.Should().NotBeNull();
            result!.Winnings.Should().Be(expectedWinnings);
            result.TotalPayout.Should().Be(expectedTotalPayout);
        }

        [Theory]
        [InlineData(-100, 100, 100, 200)]
        [InlineData(-200, 100, 50, 150)]
        [InlineData(-300, 50, 16.666666666666668, 66.66666666666667)]
        public void CalculateWinningsAndPayout_NegativeOdds_ReturnsCorrectResult(int americanOdds, int betAmount, double expectedWinnings, double expectedTotalPayout)
        {
            // Act
            var result = _calculations.CalculateWinningsAndPayout(americanOdds, betAmount);

            // Assert
            result.Should().NotBeNull();
            result!.Winnings.Should().Be(expectedWinnings);
            result.TotalPayout.Should().Be(expectedTotalPayout);
        }

        [Fact]
        public void CalculateWinningsAndPayout_InvalidOdds_ReturnsNull()
        {
            // Act
            var result = _calculations.CalculateWinningsAndPayout(0, 100);

            // Assert
            result.Should().BeNull();
        }
    }
}