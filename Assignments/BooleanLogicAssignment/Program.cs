using System;
using static BooleanLogicAssignment.ConsoleUtil;

namespace BooleanLogicAssignment {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("Car Insurance Qualifications");
            byte age = ReadNumeral<byte>("What is your age", 1, 100); // Req 200.1a
            bool hasDUI = ReadNumeral<bool>("Have you ever had a DUI", maxValue: true); // Req 200.1b
            byte ticketsCount = ReadNumeral<byte>("How many speeding tickets do you have", 0, 100); // Req 200.1c
            bool meetsQualifications = age > 15 && !hasDUI && ticketsCount <= 3;// Req 200.2a, 200.2b, 200.2c
            Console.WriteLine($"Qualified: {meetsQualifications}"); // Req 200.3
            Console.ReadKey(true);
        }
    }
}
