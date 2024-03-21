using System;
using static Assignments.ConsoleUtil;

namespace Assignments {
    public class MathAndComparisonAssignment : IAssignment {
        ushort IAssignment.Step => 187;
        string IAssignment.Name => "Math and Comparison Assignment";

        public void Invoke() {
            Console.WriteLine("# Anoymous Income Comparison Program\nPerson 1");
            int hourlyRate = ReadNumeral<int>("Hourly Rate", 0, 100),
                hoursPerWeek = ReadNumeral<int>("Hours worked per week", 0, 80),
                person1Salary = hourlyRate * hoursPerWeek * 52;
            Console.WriteLine("Person 2");
            hourlyRate = ReadNumeral<int>("Hourly Rate", 0, 100);
            hoursPerWeek = ReadNumeral<int>("Hours worked per week", 0, 80);
            int person2Salary = hourlyRate * hoursPerWeek * 52;
            Console.WriteLine($"Does Person 1 make more money than Person 2? {person1Salary > person2Salary}");
        }
    }
}
