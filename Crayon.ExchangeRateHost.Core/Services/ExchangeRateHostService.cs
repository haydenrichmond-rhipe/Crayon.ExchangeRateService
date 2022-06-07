using Crayon.ExchangeRateHost.Core.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Crayon.ExchangeRateHost.Core.Services
{
    public interface IExchangeRateHostService
    {
        Task<GetRateForDateResponse> GetRateForDateAsync(DateTimeOffset date, string sourceCurrency, string targetCurrency);
    }

    public class ExchangeRateHostService : IExchangeRateHostService
    {
        private readonly IHttpClientService _client;
        private readonly IUriBulderService _urlBulderService;
        private readonly ILogger<ExchangeRateHostService> _log;

        public ExchangeRateHostService(
            IHttpClientService client,
            IUriBulderService urlBulderService,
            ILogger<ExchangeRateHostService> log)
        {
            _client = client;
            _urlBulderService = urlBulderService;
            _log = log;
        }
        public async Task<GetRateForDateResponse> GetRateForDateAsync(DateTimeOffset date, string sourceCurrency, string targetCurrency)
        {
            try
            {
                using var client = _client.CreateHttpClient();
                var url = _urlBulderService.BuildGetExchangeRateUri(date, sourceCurrency, targetCurrency);
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GetRateForDateResponse>(responseBody);
                if (!result.Success)
                {
                    throw new Exception("Exchange rate response did not indicate success.");
                }
                return result;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "An unexpected error occurred when making call to exchange rate host service.");
                throw;
            }
        }
    }
}
