using System;
using static Assignments.ConsoleUtil;

namespace Assignments {
    public class ConsoleMathAssignment : IAssignment {
        ushort IAssignment.Step => 186;
        string IAssignment.Name => "Console Math Assignment";

        public void Invoke() {
            sbyte userValue;
            Console.WriteLine($"{userValue = ReadNumeral<sbyte>(minValue: sbyte.MinValue, maxValue: sbyte.MaxValue)} * 50 = {userValue * 50}");     // Req 186.1
            Console.WriteLine($"{userValue = ReadNumeral<sbyte>(minValue: sbyte.MinValue, maxValue: sbyte.MaxValue)} + 25 = {userValue + 25}");     // Req 186.2
            Console.WriteLine($"{userValue = ReadNumeral<sbyte>(minValue: sbyte.MinValue, maxValue: sbyte.MaxValue)} / 12.5 = {userValue / 12.5}"); // Req 186.3
            Console.WriteLine($"{userValue = ReadNumeral<sbyte>(minValue: sbyte.MinValue, maxValue: sbyte.MaxValue)} > 50 = {userValue > 50}");     // Req 186.4
            Console.WriteLine($"{userValue = ReadNumeral<sbyte>(minValue: sbyte.MinValue, maxValue: sbyte.MaxValue)} / 7 = {userValue / 7}");       // Req 186.5
            Console.Read();
        }
    }
}
