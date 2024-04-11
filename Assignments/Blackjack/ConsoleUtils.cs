using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack {
    public static class ConsoleUtils {
        public static bool TryParse<T>(string input, out T result) where T : IConvertible {
            result = default;
            try {
                result = (T)Convert.ChangeType(input, typeof(T));
                return true;
            }
            catch { }
            return false;
        }

        public static T ReadEnum<T>(string prompt = "Enter a value") where T : struct, Enum, IConvertible {
            Type t = typeof(T); // Self explanitory
            T[] values = Enum.GetValues(t).Cast<T>().ToArray();
            return ReadEnum<T>(prompt, values.Min(), values.Max());
        }

        public static T ReadEnum<T>(string prompt, T minValue, T maxValue) where T : struct, Enum, IConvertible {
            string parseStr =  null; T result; // Defines parse string and result, used outside of do
            Type enumType = typeof(T); // Self explanitory
            T[] values = Enum.GetValues(enumType).Cast<T>().ToArray(); // Self explanitory
            bool sequentialIteration = false; // Added this after noticed req 4
            do {
                if (parseStr != null) ClearLastLine();
                if (sequentialIteration) Console.WriteLine("Please enter an actual day of the week.");
                Console.Write($"{prompt} ({minValue} - {maxValue}) ");
                parseStr = Console.ReadLine();
            }
            while (sequentialIteration = // Redo if..
                !Enum.TryParse<T>(parseStr, true, out result) // String parse fails.. and
                && !(TryParse<int>(parseStr, out int numeral) && values.Contains(result = (T)Convert.ChangeType(numeral, enumType))) // Int parse fails
            );
            return result;
        }

        /// <summary>Gets a parses a number from the console, you need to specify the min/max values since defaults are 0</summary>
        /// <typeparam name="T">The type of number to return</typeparam>
        /// <param name="prompt">The prompt of what to ask from the user</param>
        /// <param name="minValue">Lowest value (inclusive)</param>
        /// <param name="maxValue">Highest value (inclusive)</param>
        /// <param name="defaultValue">The default value returned if the user presses enter without entering any data</param>
        /// <returns>A number between minValue and maxValue (inclusivly)</returns>
        public static T ReadNumeral<T>(string prompt = "Enter a number", T minValue = default, T maxValue = default, T? defaultValue = null) where T : struct, IComparable, IConvertible {
            T outValue; string parseStr = null; bool parsed;
            string helpStr = $"({minValue} - {maxValue}){(defaultValue.HasValue ? $" (default: {defaultValue.Value})" : string.Empty)}";
            do {
                if (parseStr != null) ClearLastLine();
                Console.Write($"{prompt} {helpStr} ");
                parseStr = Console.ReadLine();
            }
            while (!(parsed = TryParse<T>(parseStr, out outValue) // Try parsing the number
                && minValue.CompareTo(outValue) <= 0 && maxValue.CompareTo(outValue) >= 0) // Ensure its within the given range
                && !(string.IsNullOrEmpty(parseStr) && defaultValue.HasValue) // Use default value only when parse string is empty
            );
            return parsed ? outValue : defaultValue.Value;
        }

        public static void ClearLastLine() {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            // Clear the line
            Console.Write(new string(' ', Console.WindowWidth));
            // Move the cursor back to the beginning of the line
            Console.SetCursorPosition(0, Console.CursorTop);
        }
    }
}
