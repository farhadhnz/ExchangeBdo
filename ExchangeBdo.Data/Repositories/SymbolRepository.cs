using ExchangeBdo.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ExchangeBdo.Data.Repositories
{
    public class SymbolRepository : ISymbolRepository
    {
        private readonly AppDbContext _dbContext;

        public SymbolRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddSymbolsAsync(List<Symbol> symbols)
        {
            await _dbContext.Symbols.AddRangeAsync(symbols);
            await _dbContext.SaveChangesAsync();
        }

        public bool CheckIfDataExists()
        {
            _dbContext.Database.EnsureCreated();
            try
            {
                if (_dbContext.Symbols.Any())
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Symbol>> GetSymbolsAsync()
        {
            return await _dbContext.Symbols.AsNoTracking().ToListAsync();
        }
    }
}
