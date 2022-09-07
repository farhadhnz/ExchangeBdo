using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeBdo.SharedServices
{
    public class InputValidationService : IInputValidationService
    {
        public DateTime ValidateDateInput(string input)
        {
            DateTime inputValue;
            while (!DateTime.TryParse(input, out inputValue))
            {
                Console.WriteLine("This is not a correct Date! Please Try Again");
                input = Console.ReadLine();
            }

            return inputValue;
        }

        public decimal ValidateDecimalInput(string input)
        {
            decimal inputValue;
            while (!decimal.TryParse(input, out inputValue))
            {
                Console.WriteLine("This is not a correct number!");
                input = Console.ReadLine();
            }

            return inputValue;
        }

        public string ValidateSymbolString(string input)
        {
            while (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Symbol can't be empty! Input the symbol once more");
                input = Console.ReadLine();
            }

            while (input.Length != 3)
            {
                Console.WriteLine("Symbol should be 3 letters! Input the symbol once more");
                input = Console.ReadLine();
            }

            return input.ToUpper();
        }

    }
}
