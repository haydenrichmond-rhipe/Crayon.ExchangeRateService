using System.ComponentModel.DataAnnotations;

namespace Crayon.ExchangeRateService.Contracts.Requests
{
    public class ExchangeRateRequest
    {
        /// <summary>
        /// A list of comma seperated dates in format YYYY-MM-DD to get exchange rates for.
        /// </summary>
        [Required]
        public string Dates { get; set; }
        /// <summary>
        /// The source currency
        /// </summary>
        [Required]
        public string SourceCurrency { get; set; }
        /// <summary>
        /// The target currency whcih the rate converts to
        /// </summary>
        [Required]
        public string TargetCurrency { get; set; }

    }
}
