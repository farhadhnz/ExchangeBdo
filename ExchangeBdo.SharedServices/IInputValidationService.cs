namespace ExchangeBdo.SharedServices
{
    public interface IInputValidationService
    {
        string ValidateSymbolString(string input);
        decimal ValidateDecimalInput(string input);
        DateTime ValidateDateInput(string input);
    }
}
