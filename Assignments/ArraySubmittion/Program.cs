using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArraySubmittion {
    public static class ConsoleUtil {
        /// <summary>Gets a parses a number from the console, you need to specify the min/max values since defaults are 0</summary>
        /// <typeparam name="T">The type of number to return</typeparam>
        /// <param name="prompt">The prompt of what to ask from the user</param>
        /// <param name="minValue">Lowest value (inclusive)</param>
        /// <param name="maxValue">Highest value (inclusive)</param>
        /// <returns>A number between minValue and maxValue (inclusivly)</returns>
        public static T ReadNumeral<T>(string prompt = "Enter a number", T minValue = default, T maxValue = default) where T : struct, IComparable, IConvertible {
            // It kinda sucks that nullables are not available in netstandard2.0,
            // otherwise I could have made default min/max values that are other than 0/False
            T outValue;
            string parseStr;
            bool firstAttempt = true;
            do {
                if (!firstAttempt)
                    Console.WriteLine($"Ensure to select an index between {minValue} and {maxValue} (exclusively inclusive)");
                else firstAttempt = false;
                Console.Write($"{prompt}: ({minValue} - {maxValue}) ");
                parseStr = Console.ReadLine();
            } while (!(TryParse<T>(parseStr, out outValue) && minValue.CompareTo(outValue) <= 0 && maxValue.CompareTo(outValue) >= 0));
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
    internal class Program {
        readonly static string[] Strings = new[] {
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
            };
        readonly static int[] Integers = new[] { 1, 2, 4, 8, 16, 32, 64, 128 };
        readonly static List<string> StringList = new List<string>(Strings);
        static void Main(string[] args) {
            // It asks for input, and uses the index to get a string and print to console
            Console.WriteLine(Strings[ConsoleUtil.ReadNumeral<int>("Select a string array index", 0, Strings.Length - 1)]);
            // It asks for input, and uses the index to get a int and print to console
            Console.WriteLine(Integers[ConsoleUtil.ReadNumeral<int>("Select a int index", 0, Integers.Length - 1)]);
            // It asks for input, and uses the index to get a string and print to console (but this time from a List)
            Console.WriteLine(StringList[ConsoleUtil.ReadNumeral<int>("Select a string list index", 0, StringList.Count - 1)]);
        }
    }
}
