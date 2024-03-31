﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CallingMethodsAssignment.DummyMethods;

namespace CallingMethodsAssignment {
    internal class Program {
        static void Main(string[] args) {
            int input = ReadNumeral<int>(minValue: 0, maxValue: 100);
            Console.WriteLine(
                $"Results:" +
                $"\n  Method1: {Method1(input)}" +
                $"\n  Method2: {Method2(input)}" +
                $"\n  Method3: {Method3(input)}"
            );

            Console.ReadKey(true);
        }

        /// <summary>Gets a parses a number from the console, you need to specify the min/max values since defaults are 0</summary>
        /// <typeparam name="T">The type of number to return</typeparam>
        /// <param name="prompt">The prompt of what to ask from the user</param>
        /// <param name="minValue">Lowest value (inclusive)</param>
        /// <param name="maxValue">Highest value (inclusive)</para0
        /// m>
        /// <returns>A number between minValue and maxValue (inclusivly)</returns>
        public static T ReadNumeral<T>(string prompt = "Enter a number", T minValue = default, T maxValue = default) where T : struct, IComparable, IConvertible {
            // It kinda sucks that nullables are not available in netstandard2.0,
            // otherwise I could have made default min/max values that are other than 0/False
            T outValue;
            string parseStr;
            do {
                Console.Write($"{prompt}: ({minValue} - {maxValue}) ");
                parseStr = Console.ReadLine();
            }
            while (!(TryParse<T>(parseStr, out outValue) && minValue.CompareTo(outValue) <= 0 && maxValue.CompareTo(outValue) >= 0));
            return outValue;
        }

        static bool TryParse<T>(string input, out T result) where T : IConvertible {
            result = default;
            try {
                result = (T)Convert.ChangeType(input, typeof(T));
                return true;
            }
            catch { }
            return false;
        }
    }
}
