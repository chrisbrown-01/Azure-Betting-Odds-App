using BettingOddsCalculator.Models;
using BettingOddsCalculator.Pages;
using BettingOddsCalculator.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;

namespace BettingOddsCalculatorTests
{
    public class IndexModelTests
    {
        private readonly Mock<ILogger<IndexModel>> _mockLogger;
        private readonly Mock<ICalculations> _mockCalculations;
        private readonly IndexModel _indexModel;

        public IndexModelTests()
        {
            _mockLogger = new Mock<ILogger<IndexModel>>();
            _mockCalculations = new Mock<ICalculations>();
            _indexModel = new IndexModel(_mockLogger.Object, _mockCalculations.Object);
        }

        [Fact]
        public void OnGet_InvalidModelState_ReturnsPageResult()
        {
            // Arrange
            _indexModel.ModelState.AddModelError("BetAmount", "The Bet Amount field is required.");

            // Act
            var result = _indexModel.OnGet();

            // Assert
            result.Should().BeOfType<PageResult>();
            //result.Should().NotBeOfType<PageResult>();
        }

        [Fact]
        public void OnGet_ValidModelStateAndNullWinningsAndPayout_ReturnsPageResult()
        {
            // Arrange
            _indexModel.BetAmount = 100;
            _indexModel.AmericanOdds = 200;
            _mockCalculations.Setup(x => x.CalculateWinningsAndPayout(200, 100)).Returns((WinningsAndPayout?)null);

            // Act
            var result = _indexModel.OnGet();

            // Assert
            result.Should().BeOfType<PageResult>();
        }

        [Fact]
        public void OnGet_ValidModelStateAndNonNullWinningsAndPayout_ReturnsPageResult()
        {
            // Arrange
            _indexModel.BetAmount = 100;
            _indexModel.AmericanOdds = 200;
            var winningsAndPayout = new WinningsAndPayout { TotalPayout = 300, Winnings = 200 };
            _mockCalculations.Setup(x => x.CalculateWinningsAndPayout(200, 100)).Returns(winningsAndPayout);

            // Act
            var result = _indexModel.OnGet();

            // Assert
            result.Should().BeOfType<PageResult>();
            _indexModel.TotalPayout.Should().Be(300);
            _indexModel.Winnings.Should().Be(200);
        }
    }
}