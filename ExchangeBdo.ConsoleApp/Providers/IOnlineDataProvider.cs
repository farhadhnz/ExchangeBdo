namespace ExchangeBdo.ConsoleApp.Providers
{
    public interface IOnlineDataProvider
    {
        Task<decimal> CalculateExchange(string baseCurrency, string targetCurrency, decimal inputValue);
    }
}
