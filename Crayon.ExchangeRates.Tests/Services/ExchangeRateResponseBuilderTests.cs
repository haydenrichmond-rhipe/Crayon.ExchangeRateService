using Crayon.ExchangeRateHost.Core.Models;
using Crayon.ExchangeRateHost.Core.Services;
using Crayon.ExchangeRateHost.Core.Settings;
using Crayon.ExchangeRates.Services;
using Crayon.ExchangeRateService.Contracts.Responses;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using TestStack.BDDfy;

namespace Crayon.ExchangeRates.Tests.Services
{
    [TestFixture]
    public class ExchangeRateResponseBuilderTests
    {

        private ExchangeRateResponseBuilder _sut;
        private List<GetRateForDateResponse> _givenRates;

        private const string _targetCurrency = "SEK";
        private ExchangeRateResponse _result;

        [SetUp]
        public void Setup()
        {
            _givenRates = new List<GetRateForDateResponse>();
            _sut = new ExchangeRateResponseBuilder();
        }

        [Test]
        public void ExchangeRateResponseBuiltAsExpected_WithOneInput()
        {
            this.Given(x => x.GivenRate(new DateTimeOffset(2018, 02, 01, 0, 0, 0, TimeSpan.Zero), 0.4m))
                .When(x => x.WhenBuildResponse())
                .Then(x => x.ThenResultIs(
                    0.4m,
                    new DateTimeOffset(2018, 02, 01, 0, 0, 0, TimeSpan.Zero),
                    0.4m,
                    new DateTimeOffset(2018, 02, 01, 0, 0, 0, TimeSpan.Zero),
                    0.4m
                    ))
                .BDDfy();
        }


        [Test]
        public void ExchangeRateResponseBuiltAsExpected_WithThreeInputs()
        {
            this.Given(x => x.GivenRate(new DateTimeOffset(2018, 02, 01, 0, 0, 0, TimeSpan.Zero), 0.4m))
                .And(x => x.GivenRate(new DateTimeOffset(2018, 03, 01, 0, 0, 0, TimeSpan.Zero), 0.3m))
                .And(x => x.GivenRate(new DateTimeOffset(2020, 03, 05, 0, 0, 0, TimeSpan.Zero), 25m))
                .When(x => x.WhenBuildResponse())
                .Then(x => x.ThenResultIs(
                    0.3m,
                    new DateTimeOffset(2018, 03, 01, 0, 0, 0, TimeSpan.Zero),
                    25m,
                    new DateTimeOffset(2020, 03, 05, 0, 0, 0, TimeSpan.Zero),
                    8.566666666666666666666666667m
                    ))
                .BDDfy();
        }

        //Arrange
        private void GivenRate(DateTimeOffset date, decimal rate)
        {
            _givenRates.Add(new GetRateForDateResponse()
            {
                Date = date,
                Rates = new Dictionary<string, decimal>() { { _targetCurrency, rate } },
            });
        }

        // Act
        private void WhenBuildResponse()
        {
            _result = _sut.Build(_givenRates, _targetCurrency);
        }

        //Assert
        private void ThenResultIs(decimal expectedMin,
            DateTimeOffset expectedMinDate, 
            decimal expectedMax,
            DateTimeOffset expectedMaxDate,
            decimal expectedAvg)
        {
            // ApprovalTests better to compare this.
            _result.MinRate.Rate.ShouldBe(expectedMin);
            _result.MinRate.Date.ShouldBe(expectedMinDate);
            _result.MaxRate.Rate.ShouldBe(expectedMax);
            _result.MaxRate.Date.ShouldBe(expectedMaxDate);
            _result.AverageRate.ShouldBe(expectedAvg);
        }
    }
}