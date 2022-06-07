using Crayon.ExchangeRateHost.Core.Settings;
using Microsoft.Extensions.Configuration;
using System;

namespace Crayon.ExchangeRateHost.Core.Services
{
    public interface IUriBulderService
    {
        string BuildGetExchangeRateUri(DateTimeOffset date, string sourceCurrency, string targetCurrency);
    }
    public class UriBulderService : IUriBulderService
    {
        private readonly IConfiguration _config;
        private const string DateFormatter = "yyyy-MM-dd";
        public UriBulderService(IConfiguration configuration)
        {
            _config = configuration;
        }
        public string BuildGetExchangeRateUri(DateTimeOffset date, string sourceCurrency, string targetCurrency)
        {
            return $"{_config[ExchangeRateHostSettings.Url]}/{date.ToString(DateFormatter)}?base={sourceCurrency}&symbols={targetCurrency}";
        }
    }
}
