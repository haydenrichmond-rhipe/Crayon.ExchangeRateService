using Crayon.ExchangeRateHost.Core.Services;
using Crayon.ExchangeRateHost.Core.Settings;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System;
using TestStack.BDDfy;

namespace Crayon.ExchangeRateHost.Core.Tests.Services
{
    [TestFixture]
    public class UriBulderServiceTests
    {

        private IUriBulderService _sut;
        private IConfiguration _config;
        private string _result;

        [SetUp]
        public void Setup()
        {
            _config = Substitute.For<IConfiguration>();
            _sut = new UriBulderService(_config);
        }

        [TestCaseSource(nameof(TestCases))]
        public void BuildsUriAsExpected(string url, DateTimeOffset date, string sourceCurrency, string targetCurrency, string expected)
        {
            this.Given(x => x.GivenConfig(url))
                .When(x => x.WhenBuildUri(date, sourceCurrency, targetCurrency))
                .Then(x => x.ThenResultIs(expected))
                .BDDfy();
        }

        private static readonly TestCaseData[] TestCases =
            {
                new TestCaseData(
                    "https://myurl",
                    new DateTimeOffset(2018, 2, 1, 0, 0, 0, TimeSpan.Zero),
                    "SEK",
                    "NOK",
                    "https://myurl/2018-02-01?base=SEK&symbols=NOK"
                    ),
                new TestCaseData(
                    "https://myUniqueUrl.com",
                    new DateTimeOffset(2018, 2, 15, 0, 0, 0, TimeSpan.Zero),
                    "SEK",
                    "NOK",
                    "https://myUniqueUrl.com/2018-02-15?base=SEK&symbols=NOK"
                    ),
                new TestCaseData(
                    "https://myUniqueUrl.com",
                    new DateTimeOffset(2020, 3, 15, 0, 0, 0, TimeSpan.Zero),
                    "SEK",
                    "NOK",
                    "https://myUniqueUrl.com/2020-03-15?base=SEK&symbols=NOK"
                    ),
                new TestCaseData(
                    "https://myUniqueUrl.com",
                    new DateTimeOffset(2020, 3, 15, 0, 0, 0, TimeSpan.Zero),
                    "USD",
                    "AUD",
                    "https://myUniqueUrl.com/2020-03-15?base=USD&symbols=AUD"
                    )
            };

        //Arrange
        private void GivenConfig(string givenUrl)
        {
            _config[ExchangeRateHostSettings.Url].Returns(givenUrl);
        }

        // Act
        private void WhenBuildUri(DateTimeOffset date, string sourceCurrency, string targetCurrency)
        {
            _result = _sut.BuildGetExchangeRateUri(date, sourceCurrency, targetCurrency);
        }

        //Assert
        private void ThenResultIs(string expected)
        {
            _result.ShouldBe(expected);
        }
    }
}