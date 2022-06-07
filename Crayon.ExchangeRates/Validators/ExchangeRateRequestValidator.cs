using Crayon.ExchangeRateService.Contracts.Requests;
using System;
using System.Linq;

namespace Crayon.ExchangeRates.Validators
{
    public interface IExchangeRateRequestValidator
    {
        ValidationResult Validate(ExchangeRateRequest request);
    }
    public class ExchangeRateRequestValidator : IExchangeRateRequestValidator
    {
        public ValidationResult Validate(ExchangeRateRequest request)
        {
            var dates = request.Dates.Split(',').Where(x => !DateTime.TryParse(x, out _));
            if (dates.Any())
            {
                return new ValidationResult() {
                    IsValid = false,
                    ValidationFailureReason = $"These dates are invalid: {string.Join(',', dates)}"
                    };
            }

            // Could also validate currencies are valid against a known set.

            return new ValidationResult() { IsValid = true };
        }
    }
}
