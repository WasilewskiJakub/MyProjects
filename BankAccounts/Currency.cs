using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ZabwazIO
{
    public enum CurrencyType
    {
        USD, PLN, EUR
    }
    [Serializable]
    public class Currency
    {
        public CurrencyType Name { get; set; }
        public double Amount { get; set; }

        public static double Exchange(double value, CurrencyType from, CurrencyType to)
        {
            if (from == to) return value;

            //convert to PLN.
            if (from == CurrencyType.EUR)
                value *= 4.53;
            if (from == CurrencyType.USD)
                value *= 4.0;
            //Convrt to target value.
            if (to == CurrencyType.PLN)
                return value;
            if (to == CurrencyType.EUR)
                return value / 4.53;
            if (to == CurrencyType.USD)
                return value / 4.0;
            return 0;
        }
    }
  
}

