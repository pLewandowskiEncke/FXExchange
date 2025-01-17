using FXExchange.Core.Interfaces;
using FXExchange.Core.Services;
using FluentAssertions;
using Xunit;

namespace FXExchange.Tests.Services
{
    public class FXRatesRetrievalServiceTests
    {
        private readonly IFXRatesRetrievalService _service;

        public FXRatesRetrievalServiceTests()
        {
            _service = new FXRatesRetrievalService();
        }

        [Fact]
        public async Task GetRatesAsync_ShouldReturnExchangeRates()
        {
            // Act
            var result = await _service.GetRatesAsync();

            // Assert
            result.Should().ContainKey("EUR").WhoseValue.Should().Be(743.94);
            result.Should().ContainKey("USD").WhoseValue.Should().Be(663.11);
            result.Should().ContainKey("GBP").WhoseValue.Should().Be(852.85);
            result.Should().ContainKey("SEK").WhoseValue.Should().Be(76.10);
            result.Should().ContainKey("NOK").WhoseValue.Should().Be(78.40);
            result.Should().ContainKey("CHF").WhoseValue.Should().Be(683.58);
            result.Should().ContainKey("JPY").WhoseValue.Should().Be(5.9740);
            result.Should().ContainKey("DKK").WhoseValue.Should().Be(100.0);
        }
    }
}
