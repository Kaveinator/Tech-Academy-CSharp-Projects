using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ClassAssignment.ConsoleUtils;

namespace ClassAssignment {
    internal class Program {
        static void Main(string[] args) {
            // Fulfills Requirements
            //  - 286.2: Use the class created in 286.1
            //  - 286.2: Have the user enter a number. Call the method on that number. Display the output to the screen. It should be the entered number, divided by two. 
            new Dummy().DivideByTwo<int>(ReadNumeral<int>(maxValue: 100), out int output);
            Console.WriteLine($"Output: {output}"); // Logs the output :tada:

            Console.ReadKey(true);
        }

    }
    public class Dummy {
        // Does what the method says, it divides by two
        // Fulfils Requirements:
        //  - 286.1: Create a class with a void method that divides by two with an output
        //  - 286.4: Create a method with output parameters.
        public void DivideByTwo(int input, out int output) => output = input / 2;

        // Filfills Requirements
        //  - 286.5: Overload a method.
        public void DivideByTwo<T>(T input, out T output)
            where T : struct, IComparable, IConvertible
            => output = (T)((dynamic)input / 2m);
    }
    // Filfills Requirements
    //  - 286.6: Declare a class to be static
    public static class ConsoleUtils { // Static class
        /// <summary>Gets a parses a number from the console, you need to specify the min/max values since defaults are 0</summary>
        /// <typeparam name="T">The type of number to return</typeparam>
        /// <param name="prompt">The prompt of what to ask from the user</param>
        /// <param name="minValue">Lowest value (inclusive)</param>
        /// <param name="maxValue">Highest value (inclusive)</param>
        /// <param name="defaultValue">The default value returned if the user presses enter without entering any data</param>
        /// <returns>A number between minValue and maxValue (inclusivly)</returns>
        public static T ReadNumeral<T>(string prompt = "Enter a number", T minValue = default, T maxValue = default, T? defaultValue = null) where T : struct, IComparable, IConvertible {
            T outValue; string parseStr; bool parsed;
            string helpStr = $"({minValue} - {maxValue}){(defaultValue.HasValue ? $" (default: {defaultValue.Value})" : string.Empty)}";
            do {
                Console.Write($"{prompt}: {helpStr} ");
                parseStr = Console.ReadLine();
            }
            while (!(parsed = TryParse<T>(parseStr, out outValue) // Try parsing the number
                && minValue.CompareTo(outValue) <= 0 && maxValue.CompareTo(outValue) >= 0) // Ensure its within the given range
                && !(string.IsNullOrEmpty(parseStr) && defaultValue.HasValue) // Use default value only when parse string is empty
            );
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
}
