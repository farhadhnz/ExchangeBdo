using ExchangeBdo.Data.Models;
using ExchangeBdo.Data.Repositories;
using ExchangeBdo.SharedDtos;
using ExchangeBdo.SharedServices;
using Microsoft.Extensions.Logging;

namespace ExchangeBdo.SchedulingApp
{
    public class ExchangeService : IExchangeService
    {
        private readonly ISymbolRepository _symbolRepository;
        private readonly IApiFetchingService<ExchangeDto> _exchangeFetchingApi;
        private readonly IExchangeRepository _exchangeRepository;
        private readonly ILogger<ExchangeService> _logger;

        public ExchangeService(ISymbolRepository symbolRepository,
            IApiFetchingService<ExchangeDto> exchangeFetchingApi,
            IExchangeRepository exchangeRepository,
            ILogger<ExchangeService> logger)
        {
            _symbolRepository = symbolRepository;
            _exchangeFetchingApi = exchangeFetchingApi;
            _exchangeRepository = exchangeRepository;
            _logger = logger;
        }

        public async Task SaveExchangeData(string baseCurrency)
        {
            // Get list of symbols 
            /// TODO: Check caching possibility
            var symbols = await _symbolRepository.GetSymbolsAsync();

            var symbolsStrList = string.Join(',', symbols
                //.Except(symbols
                //.Where(s => s.Character.Equals(baseCurrency)))
                .Select(s => s.Character));

            // Call latest API
            var headers = new Dictionary<string, string>();

            var url = $"latest?symbols={symbolsStrList}&base={baseCurrency}";
            var apiResult = await _exchangeFetchingApi.FetchDataAsync(headers, url);

            if (!apiResult.Success || apiResult.ResultValue.Rates == null)
            {
                _logger.LogError("Some Problem Occured, Please Try Again!");
                throw new Exception();
            }

            // Convert exchangeDto to List of Exchange Model
            var exchanges = new List<Exchange>();
            foreach (var exRate in apiResult.ResultValue.Rates)
            {
                var exchange = new Exchange()
                {
                    BaseCurrencyId = symbols.FirstOrDefault(x => x.Character.Equals(baseCurrency)).Id,
                    DateAdded = DateTime.UtcNow,
                    TargetCurrencyId = symbols.FirstOrDefault(x => x.Character.Equals(exRate.Key)).Id,
                    Rate = exRate.Value
                };
                exchanges.Add(exchange);
            }

            // Add Exchanges to Db
            await _exchangeRepository.SaveExchangeDataAsync(exchanges);
        }
    }

    public interface IExchangeService
    {
        Task SaveExchangeData(string baseCurrency);
    }
}
