using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodClassAssignment {
    internal class Program {
        static void Main(string[] args) {
            DummyClass dummy = new DummyClass();
            dummy.Method1(i: 123, o: 456);

            Console.ReadKey(intercept: true);
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
    internal class DummyClass {
        /// <summary>Does nothing specifc</summary>
        /// <param name="i">Does a math operation (but does not use) this</param>
        /// <param name="o">Writes this to the screen</param>
        public void Method1(int i, int o) {
            i ^= 2;
            Console.WriteLine(o);
        }
    }
}
