using System;

namespace Crayon.ExchangeRateService.Contracts.Models
{
    public class ExchangeRateModel
    {
        public DateTimeOffset Date { get; set; }
        public decimal Rate { get; set; }
    }
}
