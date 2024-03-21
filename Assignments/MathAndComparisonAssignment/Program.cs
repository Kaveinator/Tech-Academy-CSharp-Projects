using System;
using static MathAndComparisonAssignment.ConsoleUtil;

namespace MathAndComparisonAssignment {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("# Anonymous Income Comparison Program\nPerson 1");
            int hourlyRate = ReadNumeral<int>("Hourly Rate", 0, 100),
                hoursPerWeek = ReadNumeral<int>("Hours worked per week", 0, 80),
                person1Salary = hourlyRate * hoursPerWeek * 52;
            Console.WriteLine($"Annual salary of Person 1: {person1Salary}\nPerson 2");
            hourlyRate = ReadNumeral<int>("Hourly Rate", 0, 100);
            hoursPerWeek = ReadNumeral<int>("Hours worked per week", 0, 80);
            int person2Salary = hourlyRate * hoursPerWeek * 52;
            Console.WriteLine($"Annual salary of Person 2: {person2Salary}\nDoes Person 1 make more money than Person 2? {person1Salary > person2Salary}");
            Console.ReadKey(true);
        }
    }
}
