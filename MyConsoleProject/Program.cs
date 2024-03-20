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

            int userValue;
            Console.WriteLine($"{userValue = GetIntFromUser()} * 50 = {userValue * 50}");     // Req 186.1
            Console.WriteLine($"{userValue = GetIntFromUser()} + 25 = {userValue + 25}");     // Req 186.2
            Console.WriteLine($"{userValue = GetIntFromUser()} / 12.5 = {userValue / 12.5}"); // Req 186.3
            Console.WriteLine($"{userValue = GetIntFromUser()} > 50 = {userValue > 50}");     // Req 186.4
            Console.WriteLine($"{userValue = GetIntFromUser()} / 7 = {userValue / 7}");       // Req 186.5
            Console.Read();
        }

        /// <summary>Asks the user for a int number, if parse fails, it asks again</summary>
        /// <param name="prompt">What are you askng for?</param>
        /// <returns>A int value based on user input</returns>
        static int GetIntFromUser(string prompt = "Enter a number") {
            string parseStr = null;
            int outValue;
            while (parseStr == null || !int.TryParse(parseStr, out outValue)) {
                Console.Write($"{prompt}: ({int.MinValue} - {int.MaxValue}) ");
                parseStr = Console.ReadLine();
            }
            return outValue;
        }
    }
}
