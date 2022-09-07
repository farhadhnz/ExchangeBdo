using ExchangeBdo.Data.Repositories;
using ExchangeBdo.SharedDtos;
using ExchangeBdo.SharedServices;
using Microsoft.Extensions.Logging;

namespace ExchangeBdo.ConsoleApp.Providers
{
    public class OnlineDataProvider : IOnlineDataProvider
    {
        private readonly IExchangeRepository _exchangeRepository;
        private readonly IRateCalculationService _rateCalculationService;
        private readonly ILogger<OnlineDataProvider> _logger;

        public OnlineDataProvider(IExchangeRepository exchangeRepository, 
            IRateCalculationService rateCalculationService,
            ILogger<OnlineDataProvider> logger)
        {
            _exchangeRepository = exchangeRepository;
            _rateCalculationService = rateCalculationService;
            _logger = logger;
        }

        public async Task<decimal> CalculateExchange(string sourceCurrency, string targetCurrency, decimal inputValue)
        {
            // get data from datasource
            var sourceExchange = await _exchangeRepository.GetLatestExchange(sourceCurrency);
            var targetExchange =await _exchangeRepository.GetLatestExchange(targetCurrency);

            // calculate the output value
            return _rateCalculationService.CalculateExchangeValue(inputValue, sourceExchange.Rate, targetExchange.Rate);
        }
    }
}
