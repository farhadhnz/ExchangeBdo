using ExchangeBdo.Data.Models;

namespace ExchangeBdo.Data.Repositories
{
    public interface ISymbolRepository
    {
        bool CheckIfDataExists();
        Task AddSymbolsAsync(List<Symbol> symbols);

        Task<List<Symbol>> GetSymbolsAsync();
    }
}
