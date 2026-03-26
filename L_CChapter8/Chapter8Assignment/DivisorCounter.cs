using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter8Assignment
{
    public class DivisorCounter
    {
        public static int CountValidN(int k)
        {
            if (k <= 2) return 0;

            int[] divisors = GetDivisorCounts(k);

            int count = 0;

            for (int currentNumber = 2; currentNumber < k; currentNumber++)
            {
                if (divisors[currentNumber] == divisors[currentNumber + 1])
                {
                    count++;
                }
            }

            return count;
        }

        private static int[] GetDivisorCounts(int limit)
        {
            int[] divisorsCounts = new int[limit + 1];

            for (int divisor = 1; divisor <= limit; divisor++)
            {
                for (int multiple = divisor; multiple <= limit; multiple += divisor)
                {
                    divisorsCounts[multiple]++;
                }
            }

            return divisorsCounts;
        }
    }
}
