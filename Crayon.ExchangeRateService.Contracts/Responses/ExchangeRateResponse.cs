using Crayon.ExchangeRateService.Contracts.Models;

namespace Crayon.ExchangeRateService.Contracts.Responses
{
    public class ExchangeRateResponse
    {
        public ExchangeRateModel MinRate { get; set; }
        public ExchangeRateModel MaxRate { get; set; }
        public decimal AverageRate { get; set; }
    }
}
