using System;
using static Assignments.ConsoleUtil;

namespace Assignments {
    public class BranchingAssignment : IAssignment {
        ushort IAssignment.Step => 223;
        string IAssignment.Name => "Branching Assignment (Package Express)";

        public void Invoke() {
            Console.WriteLine("Welcome to Package Express. Please follow the instructions below."); // Req 223.1
            decimal pkgWeight = ReadNumeral<decimal>("What is the weight", 0.1m, 999); // Req 223.2
            if (pkgWeight > 50) { // Req 223.3
                Console.WriteLine("Package is too heavy to be shipped via Package Express. Have a good day");
                return;
            }
            decimal pkgWidth = ReadNumeral<decimal>("Width of package: ", 0.01m, 100), // Req 223.4
                pkgHeight = ReadNumeral<decimal>("Height of package: ", 0.01m, 100),   // Req 223.5
                pkgLength = ReadNumeral<decimal>("Length of package: ", 0.01m, 100);   // Req 223.6
            if (pkgWidth + pkgHeight + pkgLength > 50) { // Req 223.7
                Console.WriteLine("Package is too big to be shipped via Package Express. Have a good day");
                return;
            }
            decimal shipQuote = (pkgWidth * pkgHeight * pkgLength * pkgWeight) / 100m; // Req 223.8, 223.9
            Console.WriteLine($"Estimated cost to ship this package is ${shipQuote:0.00}"); // Req 223.10
        }
    }
}
