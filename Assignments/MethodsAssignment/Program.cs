using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodsAssignment {
    internal class Program {
        static void Main(string[] args) {
            int num1 = ReadNumeral<int>("Enter num1", maxValue: 100),
                num2 = ReadNumeral<int>("Enter num2", maxValue: 100, defaultValue: 1); // Get numbers
            Console.WriteLine(
                "# Tests" +
                $"\n  1: {Methods.Method1(num1)}" +
                $"\n  2: {Methods.Method1(num1, num2)}"
            );
            Console.ReadKey(true);
        }

        /// <summary>Gets a parses a number from the console, you need to specify the min/max values since defaults are 0</summary>
        /// <typeparam name="T">The type of number to return</typeparam>
        /// <param name="prompt">The prompt of what to ask from the user</param>
        /// <param name="minValue">Lowest value (inclusive)</param>
        /// <param name="maxValue">Highest value (inclusive)</para0
        /// m>
        /// <returns>A number between minValue and maxValue (inclusivly)</returns>
        public static T ReadNumeral<T>(string prompt = "Enter a number", T minValue = default, T maxValue = default, T? defaultValue = null) where T : struct, IComparable, IConvertible {
            T outValue;
            string parseStr;
            bool parsed = false;
            string helpStr = $"({minValue} - {maxValue}){(defaultValue.HasValue ? $" (default: {defaultValue.Value})" : string.Empty)}";
            do {
                Console.Write($"{prompt}: {helpStr} ");
                parseStr = Console.ReadLine();
            }
            while (!(parsed = TryParse<T>(parseStr, out outValue) && minValue.CompareTo(outValue) <= 0 && maxValue.CompareTo(outValue) >= 0) && !defaultValue.HasValue);
            return parsed ? outValue : defaultValue.Value;
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
    public class Methods {
        public static int Method1(int i, int o = 1) => i ^ o;
    }
}
