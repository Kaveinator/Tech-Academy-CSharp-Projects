using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Double;

namespace StringsAndIntegers {
    internal class Program {
        readonly static int[] Integers = new[] { 0, 1, -2, 3, 4, 5, 6, 7, 8, 9, 0}; // Req 264.1
        static void Main(string[] args) {
            // Loop over each integer
            foreach (int num in Integers) { // num => numerator
                double den = ReadNumeral<int>(minValue: -100, maxValue: 100), // read an integer but store as double so you can get non-whole numbers
                    result = num / den;
                ClearLastLine();
                // Even if the denominator is 0, dividing by double (0) causes it to be
                //  either PositiveInfitity or NegativeInfinity depending if the numerator
                //  was positive or negative,
                // If numerator or denominator are both 0, then a NaN is returned
                // Exceptions are not thrown
                Console.WriteLine($"{num} / {den} = {result} (IsPositiveInfinity: {IsPositiveInfinity(result)}; IsNegativeInfinity: {IsNegativeInfinity(result)}; IsNaN: {IsNaN(result)})");
            }

            Console.ReadKey(true);
        }

        public static void ClearLastLine() {
            int currentLineCursor = Console.CursorTop - 1;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        /// <summary>Gets a parses a number from the console, you need to specify the min/max values since defaults are 0</summary>
        /// <typeparam name="T">The type of number to return</typeparam>
        /// <param name="prompt">The prompt of what to ask from the user</param>
        /// <param name="minValue">Lowest value (inclusive)</param>
        /// <param name="maxValue">Highest value (inclusive)</para0
        /// m>
        /// <returns>A number between minValue and maxValue (inclusivly)</returns>
        public static T ReadNumeral<T>(string prompt = "Enter a number", T minValue = default, T maxValue = default) where T : struct, IComparable, IConvertible {
            // It kinda sucks that nullables are not available in netstandard2.0,
            // otherwise I could have made default min/max values that are other than 0/False
            T outValue;
            string parseStr;
            do {
                Console.Write($"{prompt}: ({minValue} - {maxValue}) ");
                parseStr = Console.ReadLine();
            }
            while (!(TryParse<T>(parseStr, out outValue) && minValue.CompareTo(outValue) <= 0 && maxValue.CompareTo(outValue) >= 0));
            return outValue;
        }

        static bool TryParse<T>(string input, out T result) where T : IConvertible {
            result = default;
            try {
                result = (T)Convert.ChangeType(input, typeof(T));
                return true;
            }
            catch { }
            return false;
        }
    }
}
