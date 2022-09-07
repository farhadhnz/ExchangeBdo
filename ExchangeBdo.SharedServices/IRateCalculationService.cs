namespace ExchangeBdo.SharedServices
{
    public interface IRateCalculationService
    {
        decimal CalculateExchangeValue(decimal inputValue, decimal sourceRate, decimal targetRate);
    }
}