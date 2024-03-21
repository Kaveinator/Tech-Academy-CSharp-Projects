using System;

namespace Assignments {
    public class DailyReportAssignment : IAssignment {
        ushort IAssignment.Step => 164;
        string IAssignment.Name => "Daily Report Assignment";

        public void Invoke() {
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
            Console.WriteLine("Thank you for your answers. An Instructor will respond to this shortly. Have a great day!");
        }
    }
}
