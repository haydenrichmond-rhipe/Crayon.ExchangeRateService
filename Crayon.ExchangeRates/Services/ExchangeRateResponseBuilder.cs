using Crayon.ExchangeRateHost.Core.Models;
using Crayon.ExchangeRateService.Contracts.Models;
using Crayon.ExchangeRateService.Contracts.Responses;
using System.Collections.Generic;
using System.Linq;

namespace Crayon.ExchangeRates.Services
{
    public interface IExchangeRateResponseBuilder
    {
        ExchangeRateResponse Build(IEnumerable<GetRateForDateResponse> rates, string targetCurrency);
    }
    public class ExchangeRateResponseBuilder : IExchangeRateResponseBuilder
    {
        public ExchangeRateResponse Build(IEnumerable<GetRateForDateResponse> rates, string targetCurrency)
        {
            var castRates = rates
                .Select(x => new { Date = x.Date, Rate = x.Rates[targetCurrency] })
                .OrderBy(x => x.Rate);

            var minRate = castRates.First();
            var maxRate = castRates.Last();
            var avgRate = castRates.Average(x => x.Rate);

            return new ExchangeRateResponse()
            {
                MinRate = new ExchangeRateModel() { Rate = minRate.Rate, Date = minRate.Date },
                MaxRate = new ExchangeRateModel() { Rate = maxRate.Rate, Date = maxRate.Date },
                AverageRate = avgRate
            };
        }
    }
}
