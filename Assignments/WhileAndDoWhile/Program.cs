using System;

namespace WhileAndDoWhile {
    internal class Program {
        static void Main(string[] args) {
            byte i = 0;
            while (i < 5) Console.WriteLine($"while: {i++}");

            i = 0;
            do Console.WriteLine($"doWhile: {i++}");
            while (i < 5);
            Console.ReadKey(true);
        }
    }
}
