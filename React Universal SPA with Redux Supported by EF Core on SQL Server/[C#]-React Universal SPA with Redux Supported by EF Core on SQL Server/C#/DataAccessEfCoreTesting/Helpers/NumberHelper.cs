using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessEfCoreTesting.Helpers
{
    public static class NumberHelper
    {
        public static IList<int> GenerateRandomIntArray(int length)
        {
            var intArray = new int[length];

            var rand = new Random();

            for (var i = 0; i < length; i++)
            {
                intArray[i] = i + 1;
            }

            for (var i = length - 1; i > 0; i--)
            {
                var j = rand.Next(0, i);

                var temp = intArray[i];
                intArray[i] = intArray[j];
                intArray[j] = temp;
            }

            return intArray;
        }
    }
}
