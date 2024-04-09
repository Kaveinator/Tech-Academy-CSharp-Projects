using System;
using static InputAssignment.ConsoleUtils;

namespace DateTimeAssignment {
    internal class Program {
        static void Main(string[] args) {
            DateTime startTime = DateTime.Now;
            Console.WriteLine(startTime); // Print current time
            sbyte hoursToAdd = ReadNumeral<sbyte>(minValue: -100, maxValue: 100);
            ClearLastLine(); // Clears the input
            Console.WriteLine(startTime.AddHours(hoursToAdd)); // Print datetime with added horus

            while (true) _ = Console.ReadKey(true);
        }
    }
}
