using Crayon.ExchangeRateHost.Core.Models;
using Crayon.ExchangeRateHost.Core.Services;
using Crayon.ExchangeRates.Validators;
using Crayon.ExchangeRateService.Contracts.Models;
using Crayon.ExchangeRateService.Contracts.Requests;
using Crayon.ExchangeRateService.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crayon.ExchangeRates.Services
{
    public interface IExchangeRateService
    {
        Task<ExchangeRateResponse> GetExchangeRateData(ExchangeRateRequest request);
    }
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly IExchangeRateHostService _exchangeRateHostService;
        private readonly IExchangeRateRequestValidator _exchangeRateRequestValidator;
        private readonly IExchangeRateResponseBuilder _exchangeRateResponseBuilder;
        public ExchangeRateService(IExchangeRateHostService exchangeRateHostService,
            IExchangeRateRequestValidator exchangeRateRequestValidator,
            IExchangeRateResponseBuilder exchangeRateResponseBuilder)
        {
            _exchangeRateHostService = exchangeRateHostService;
            _exchangeRateRequestValidator = exchangeRateRequestValidator;
            _exchangeRateResponseBuilder = exchangeRateResponseBuilder;
        }

        public async Task<ExchangeRateResponse> GetExchangeRateData(ExchangeRateRequest request)
        {
            var validationResult = _exchangeRateRequestValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                throw new Exception($"Invalid request for reason: {validationResult.ValidationFailureReason}");
            }

            var rates = new List<GetRateForDateResponse>();
            foreach (var date in request.Dates.Split(','))
            {
                rates.Add(await _exchangeRateHostService
                    .GetRateForDateAsync(
                    DateTime.Parse(date.ToString()),
                    request.SourceCurrency,
                    request.TargetCurrency));
            }

            return _exchangeRateResponseBuilder.Build(rates, request.TargetCurrency);
        }
    }
}
