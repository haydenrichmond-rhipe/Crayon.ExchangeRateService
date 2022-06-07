using Crayon.ExchangeRateHost.Core.Models;
using Crayon.ExchangeRateHost.Core.Services;
using Crayon.ExchangeRateHost.Core.Settings;
using Crayon.ExchangeRates.Services;
using Crayon.ExchangeRates.Validators;
using Crayon.ExchangeRateService.Contracts.Requests;
using Crayon.ExchangeRateService.Contracts.Responses;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using TestStack.BDDfy;

namespace Crayon.ExchangeRates.Tests.Validators
{
    [TestFixture]
    public class ExchangeRateRequestValidatorTests
    {

        private ExchangeRateRequestValidator _sut;
        private ExchangeRateRequest _givenRequest;

        private ValidationResult _result;

        [SetUp]
        public void Setup()
        {
            _sut = new ExchangeRateRequestValidator();
        }

        [TestCase("2018-01-01,asdf")]
        [TestCase("2018,01,01")]
        [TestCase(",2018-01-01")]
        [TestCase("2018-01-01,")]
        [TestCase("2018-25-01,")]
        [TestCase("20186-01-01,")]
        public void InvalidDates(string givenDates)
        {
            this.Given(x => x.GivenRequest(givenDates))
                .When(x => x.WhenValidateRequest())
                .Then(x => x.ThenResultIs(false))
                .BDDfy();
        }

        [TestCase("2018-01-01,2018-02-04")]
        [TestCase("2018-01-01,2018-02-04,2020-05-04")]
        public void ValidDates(string givenDates)
        {
            this.Given(x => x.GivenRequest(givenDates))
                .When(x => x.WhenValidateRequest())
                .Then(x => x.ThenResultIs(true))
                .BDDfy();
        }

        //Arrange
        private void GivenRequest(string dates)
        {
            _givenRequest = new ExchangeRateRequest()
            {
                Dates = dates,
                SourceCurrency = string.Empty,
                TargetCurrency = string.Empty,
            };
        }

        // Act
        private void WhenValidateRequest()
        {
            _result = _sut.Validate(_givenRequest);
        }

        //Assert
        private void ThenResultIs(bool isValid)
        {
            _result.IsValid.ShouldBe(isValid);
        }
    }
}