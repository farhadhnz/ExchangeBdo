using ExchangeBdo.SharedServices;

namespace ExchangeBdo.ConsoleApp.Providers
{
    public class InputOutputProvider : IInputOutputProvider
    {
        private readonly IInputValidationService _inputValidationService;
        private readonly IOnlineDataProvider _onlineDataProvider;

        public InputOutputProvider(IOnlineDataProvider onlineDataProvider,
            IInputValidationService inputValidationService)
        {
            _onlineDataProvider = onlineDataProvider;
            _inputValidationService = inputValidationService;
        }

        public async Task ProcessInputOutput()
        {
            Console.WriteLine("Base Currency: ");
            var baseCurrency = _inputValidationService.ValidateSymbolString(Console.ReadLine());
            Console.WriteLine("Target Currency: ");
            var targetCurrency = _inputValidationService.ValidateSymbolString(Console.ReadLine());
            Console.WriteLine("Value to Calculate: ");
            var inputValue = _inputValidationService.ValidateDecimalInput(Console.ReadLine());
            var exchangeValue = await _onlineDataProvider.CalculateExchange(baseCurrency, targetCurrency, inputValue);
            Console.WriteLine($"Output Exchange is: {exchangeValue.ToString("f3")}");
        }
    }
}
