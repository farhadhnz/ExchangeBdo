using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeBdo.SharedServices
{
    public class RateCalculationService : IRateCalculationService
    {
        public decimal CalculateExchangeValue(decimal inputValue, decimal sourceRate, decimal targetRate)
        {
            return inputValue * (targetRate / sourceRate);
        }
    }
}
