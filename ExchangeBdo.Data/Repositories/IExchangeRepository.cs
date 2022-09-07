using ExchangeBdo.Data.Models;

namespace ExchangeBdo.Data.Repositories
{
    public interface IExchangeRepository
    {
        Task SaveExchangeDataAsync(List<Exchange> exchanges);
        Task<Exchange> GetLatestExchange(string symbolCharacter);
    }
}
