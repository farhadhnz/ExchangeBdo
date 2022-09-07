using ExchangeBdo.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeBdo.Data.Repositories
{
    public class ExchangeRepository : IExchangeRepository
    {
        private readonly AppDbContext _dbContext;

        public ExchangeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Exchange> GetLatestExchange(string symbolCharacter)
        {
            var exchange = await _dbContext.Exchanges.OrderByDescending(x => x.DateAdded)
                .FirstOrDefaultAsync(x => x.TargetCurrencyId.Equals(
                    _dbContext.Symbols.FirstOrDefault(s => s.Character.Equals(symbolCharacter)).Id));

            return exchange;
        }

        public async Task SaveExchangeDataAsync(List<Exchange> exchanges)
        {
            await _dbContext.Exchanges.AddRangeAsync(exchanges);
            await _dbContext.SaveChangesAsync();
        }
    }
}
