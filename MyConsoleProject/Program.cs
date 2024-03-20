using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsoleProject {
    internal class Program {
        static void Main() {
            /*Console.Write("What is your name? ");
            string name = Console.ReadLine();
            Console.WriteLine($"Hello, {name}!");
            Console.WriteLine("Welcome to Acme Accounting Systems\nRememeber, we're \"accounting\" on you!");*/
            /*
            // Req 164.1, 164.2, 164.4a
            Console.Write("The Tech Academy\nStudent Daily Report\nWhat's your name? ");
            string studentName = Console.ReadLine();
            // Req 164.4b
            Console.Write("What course are you on? ");
            string courseName = Console.ReadLine();
            // Req 164.4c
            string tempStr = null; // A temp variable used for parsing
            while (tempStr == null || !ushort.TryParse(tempStr, out ushort stepNum)) {
                Console.Write("What step are you on? ");
                tempStr = Console.ReadLine();
            }
            // Req 164.4d
            tempStr = null; // Not really required here, but resetting parse variable
            while (tempStr == null || !bool.TryParse(tempStr, out bool needsHelp)) {
                Console.Write("Do you need help? (true/false) ");
                tempStr = Console.ReadLine();
            }
            // Req 164.4e
            Console.WriteLine("Were there any positive experiences you’d like to share? Please give specifics. ");
            string positiveExperiences = Console.ReadLine();
            // Req 164.4f
            Console.WriteLine("Is there any other feedback you’d like to provide? Please be specific. ");
            string feedback = Console.ReadLine();
            // Req 164.4g
            tempStr = null; // Reset parse variable
            while (tempStr == null || !byte.TryParse(tempStr, out byte hoursStudied)) {
                Console.Write("How many hours did you study? ");
                tempStr = Console.ReadLine();
            }

            // Req 164.5
            Console.WriteLine("Thank you for your answers. An Instructor will respond to this shortly. Have a great day!");*/

            /*Console.WriteLine(
                $"{2 + 2}\n" + // Req 172.1
                $"{2 - 2}\n" + // Req 172.2
                $"{2 * 2}\n" + // Req 172.3
                $"{2 / 2}\n" +
                $"{("a" + 2)}" // Req 178
            );*/

            /*int userValue;
            Console.WriteLine($"{userValue = GetIntFromUser()} * 50 = {userValue * 50}");     // Req 186.1
            Console.WriteLine($"{userValue = GetIntFromUser()} + 25 = {userValue + 25}");     // Req 186.2
            Console.WriteLine($"{userValue = GetIntFromUser()} / 12.5 = {userValue / 12.5}"); // Req 186.3
            Console.WriteLine($"{userValue = GetIntFromUser()} > 50 = {userValue > 50}");     // Req 186.4
            Console.WriteLine($"{userValue = GetIntFromUser()} / 7 = {userValue / 7}");       // Req 186.5
            */

            /*
            Console.WriteLine("# Anoymous Income Comparison Program\nPerson 1");
            int hourlyRate = GetIntFromUser("Hourly Rate", 0, 100),
                hoursPerWeek = GetIntFromUser("Hours worked per week", 0, 80),
                person1Salary = hourlyRate * hoursPerWeek * 52;
            Console.WriteLine("Person 2");
            hourlyRate = GetIntFromUser("Hourly Rate", 0, 100);
            hoursPerWeek = GetIntFromUser("Hours worked per week", 0, 80);
            int person2Salary = hourlyRate * hoursPerWeek * 52;
            Console.WriteLine($"Does Person 1 make more money than Person 2? {person1Salary > person2Salary}");
            */

            /*Console.WriteLine("Car Insurance Qualifications");
            int age = GetIntFromUser("What is your age", 1, 100); // Req 200.1a
            bool hasDUI = ReadBool("Have you ever had a DUI"); // Req 200.1b
            int ticketsCount = GetIntFromUser("How many speeding tickets do you have", 0, 100); // Req 200.1c
            bool meetsQualifications = age > 15 && !hasDUI && ticketsCount <= 3;// Req 200.2a, 200.2b, 200.2c
            Console.WriteLine($"Qualified: {meetsQualifications}"); // Req 200.3
            */

            RunPackageExpress();
            Console.Read();
        }

        static void RunPackageExpress() {
            Console.WriteLine("Welcome to Package Express. Please follow the instructions below."); // Req 223.1
            decimal pkgWeight = GetDecimalFromUser("What is the weight", 0.1m, 999); // Req 223.2
            if (pkgWeight > 50) { // Req 223.3
                Console.WriteLine("Package is too heavy to be shipped via Package Express. Have a good day");
                return;
            }
            decimal pkgWidth = GetDecimalFromUser("Width of package: ", 0.01m, 100), // Req 223.4
                pkgHeight = GetDecimalFromUser("Height of package: ", 0.01m, 100),   // Req 223.5
                pkgLength = GetDecimalFromUser("Length of package: ", 0.01m, 100);   // Req 223.6
            if (pkgWidth + pkgHeight + pkgLength > 50) { // Req 223.7
                Console.WriteLine("Package is too big to be shipped via Package Express. Have a good day");
                return;
            }
            decimal shipQuote = (pkgWidth * pkgHeight * pkgLength * pkgWeight) / 100m; // Req 223.8, 223.9
            Console.WriteLine($"Estimated cost to ship this package is ${shipQuote:0.00}"); // Req 223.10
        }

        /// <summary>Asks the user for a int number, if parse fails, it asks again</summary>
        /// <param name="prompt">What are you askng for?</param>
        /// <returns>A int value based on user input</returns>
        static int GetIntFromUser(string prompt = "Enter a number", int minValue = int.MinValue, int maxValue = int.MaxValue) {
            string parseStr = null;
            int outValue;
            while (parseStr == null || !(int.TryParse(parseStr, out outValue) && minValue <= outValue && outValue <= maxValue)) {
                Console.Write($"{prompt}: ({minValue} - {maxValue}) ");
                parseStr = Console.ReadLine();
            }
            return outValue;
        }
        /// <summary>Asks the user for a int number, if parse fails, it asks again</summary>
        /// <param name="prompt">What are you askng for?</param>
        /// <returns>A int value based on user input</returns>
        static decimal GetDecimalFromUser(string prompt = "Enter a number", decimal minValue = decimal.MinValue, decimal maxValue = decimal.MaxValue) {
            string parseStr = null;
            decimal outValue;
            while (parseStr == null || !(decimal.TryParse(parseStr, out outValue) && minValue <= outValue && outValue <= maxValue)) {
                Console.Write($"{prompt}: ({minValue} - {maxValue}) ");
                parseStr = Console.ReadLine();
            }
            return outValue;
        }
        /// <summary>Asks the user for a bool value, if parse fails, it asks again, if an empty string is passed it will try returning a suggested value (when supplied)</summary>
        /// <param name="prompt">What are you asking for?</param>
        /// <param name="suggestedValue">The value returned if the user doesn't enter any data</param>
        /// <returns>A true/false value from user input or fallback value</returns>
        static bool ReadBool(string prompt = "Enter a number", bool? suggestedValue = null) {
            string parseStr = null,
                options = suggestedValue.HasValue ? $"({(suggestedValue.Value ? "True/false" : "true/False")})" : "true/false";
            bool outValue;
            while (parseStr == null || !bool.TryParse(parseStr, out outValue)) {
                Console.Write($"{prompt}: ({options}) ");
                parseStr = Console.ReadLine();
                if (string.IsNullOrEmpty(parseStr) && suggestedValue.HasValue)
                    return suggestedValue.Value;
            }
            return outValue;
        }
    }
}
