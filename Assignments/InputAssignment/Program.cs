using System;
using System.IO;
using static InputAssignment.ConsoleUtils;

namespace InputAssignment {
    internal class Program {
        static void Main(string[] args) {
            LogFile logger = new LogFile(); // Initilize a log file
            logger.Log(ReadNumeral<sbyte>(minValue: -100, maxValue: 100)); // Get num from -100 to 100
            ClearLastLine(); // Erase the question for input
            logger.Dispose(); // Dispose the log file since no longer needed
            Console.WriteLine(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "latest.log"))); // Display the log file

            while (true) _ = Console.ReadKey(true); // Keeps console infinitly open
        }
    }
}
