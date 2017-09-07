using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1404Helper
{
    class Calculator
    {
        public static decimal calculate(decimal[] satisfies, decimal[] inhabitants)
        {
            decimal amount = 0;
            for (int i = 0; i < satisfies.Length; i++)
            {
                if (satisfies[i] != 0)
                {
                    amount += inhabitants[i] / satisfies[i];
                }
            }
            return amount;
        }
    }
}
