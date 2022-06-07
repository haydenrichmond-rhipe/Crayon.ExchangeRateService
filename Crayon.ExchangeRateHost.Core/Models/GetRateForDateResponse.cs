using System;
using System.Collections.Generic;

namespace Crayon.ExchangeRateHost.Core.Models
{
    public class GetRateForDateResponse
    {
        public bool Success { get; set; }
        /// <summary>
        /// Key = TargetCurrency, Value = Rate
        /// </summary>
        public Dictionary<string, decimal> Rates { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
