using BettingOddsCalculator.Pages;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq.Protected;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace BettingOddsCalculatorTests
{
    public class SearchMmaFighterModelTests
    {
        private readonly Mock<IHttpClientFactory> _mockHttpClientFactory;
        private readonly Mock<ILogger<SearchMmaFighterModel>> _mockLogger;
        private readonly SearchMmaFighterModel _searchMmaFighterModel;

        public SearchMmaFighterModelTests()
        {
            _mockHttpClientFactory = new Mock<IHttpClientFactory>();
            _mockLogger = new Mock<ILogger<SearchMmaFighterModel>>();
            _searchMmaFighterModel = new SearchMmaFighterModel(_mockLogger.Object, _mockHttpClientFactory.Object);
        }

        [Fact]
        public async Task OnGetAsync_InvalidModelState_ReturnsPageResult()
        {
            // Arrange
            _searchMmaFighterModel.ModelState.AddModelError("FighterName", "The Fighter Name field is required.");

            // Act
            var result = await _searchMmaFighterModel.OnGetAsync();

            // Assert
            result.Should().BeOfType<PageResult>();
        }

        [Fact]
        public async Task OnGetAsync_UnsuccessfulResponse_ReturnsPageResult()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            httpClient.BaseAddress = new Uri("https://example.com"); // Set the BaseAddress property to a valid URI
            _mockHttpClientFactory.Setup(x => x.CreateClient("SearchMmaFighter")).Returns(httpClient);

            // Act
            var result = await _searchMmaFighterModel.OnGetAsync();

            // Assert
            result.Should().BeOfType<PageResult>();
            _searchMmaFighterModel.MmaMatchups.Should().BeEmpty();
        }

        [Fact]
        public async Task OnGetAsync_SuccessfulResponseAndNonEmptyMatchups_ReturnsPageResult()
        {
            // Arrange
            var mmaMatchupsJson = "[{\"EventName\":\"UFC 300\",\"Odds\":1.5}]";
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(mmaMatchupsJson) });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            httpClient.BaseAddress = new Uri("https://example.com"); // Set the BaseAddress property to a valid URI
            _mockHttpClientFactory.Setup(x => x.CreateClient("SearchMmaFighter")).Returns(httpClient);

            // Act
            var result = await _searchMmaFighterModel.OnGetAsync();

            // Assert
            result.Should().BeOfType<PageResult>();
            _searchMmaFighterModel.MmaMatchups.Should().HaveCount(1);
        }
    }
}