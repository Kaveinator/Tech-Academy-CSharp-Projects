using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingEnumsAssignment {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine($"You entered: {ReadEnum<DayOfWeek>("Whats the current day", minValue: DayOfWeek.Sunday, maxValue: DayOfWeek.Saturday)}");

            DayOfWeek invalidDay = (DayOfWeek)8;
            // One inaccuracy I found was that Enums constrain the name to value,
            // but enums can still hold values outside of their constraints.
            // Enums essentially assign names to numeral values, but developers
            // should treat them as integers and potentially prone to invalid values
            Console.WriteLine(invalidDay);

            Console.ReadKey(true);

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

        public static T ReadEnum<T>(string prompt = "Enter a value", T minValue = default, T maxValue = default) where T : struct, Enum, IConvertible {
            string parseStr; T result; // Defines parse string and result, used outside of do
            Type enumType = typeof(T); // Self explanitory
            T[] values = Enum.GetValues(enumType).Cast<T>().ToArray(); // Self explanitory
            bool sequentialIteration = false; // Added this after noticed req 4
            do {
                if (sequentialIteration) Console.WriteLine("Please enter an actual day of the week.");
                Console.Write($"{prompt}: ({minValue} - {maxValue}) ");
                parseStr = Console.ReadLine();
            }
            while (sequentialIteration = // Redo if..
                !Enum.TryParse<T>(parseStr, true, out result) // String parse fails.. and
                && !(TryParse<int>(parseStr, out int numeral) && values.Contains(result = (T)Convert.ChangeType(numeral, enumType))) // Int parse fails
            );
            return result;
        }
    }
    enum Days {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }
}
