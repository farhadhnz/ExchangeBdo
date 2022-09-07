using ExchangeBdo.Data.Models;
using ExchangeBdo.Data.Repositories;
using ExchangeBdo.SharedDtos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeBdo.SharedServices
{
    public class DataFeederService : IDataFeederService
    {
        private readonly ISymbolRepository _symbolRepository;
        private readonly IApiFetchingService<SymbolDto> _symbolApiService;
        private readonly ILogger<DataFeederService> _logger;

        public DataFeederService(ISymbolRepository symbolRepository,
            IApiFetchingService<SymbolDto> symbolApiService,
            ILogger<DataFeederService> logger)
        {
            _symbolRepository = symbolRepository;
            _symbolApiService = symbolApiService;
            _logger = logger;
        }

        public async Task FeedSymbols()
        {
            if (_symbolRepository.CheckIfDataExists())
                return;

            var headers = new Dictionary<string, string>();
         
            var url = "symbols";
            var apiResult = await _symbolApiService.FetchDataAsync(headers, url);

            if (apiResult.Success)
            {
                _logger.LogInformation("Symbols Fetched...");

                var symbolDto = apiResult.ResultValue;

                var symbols = new List<Symbol>();
                foreach (var symbolDtoItem in symbolDto.Symbols)
                {
                    symbols.Add(new Symbol
                    {
                        Character = symbolDtoItem.Key,
                        Name = symbolDtoItem.Value
                    });
                }
                await _symbolRepository.AddSymbolsAsync(symbols);

                _logger.LogInformation("Symbols Added to Db...");
            }
            else
            {
                _logger.LogError("A problem Occured while fetching data...");
            }
        }
    }

    public interface IDataFeederService
    {
        Task FeedSymbols();
    }
}
