using System;

namespace StructAssignment {
    internal class Program {
        static void Main(string[] args) {
            Number<decimal> number = new Number<decimal>(21m); // Create a number struct with a decimal value of 21
            Console.WriteLine(number); // Print
            Console.ReadKey(true); // Wait for key press then exit
        }
    }
    public struct Number<T> {
        public T Ammount;
        public Number(T ammt) => Ammount = ammt;
        public override string ToString() => Ammount.ToString();
    }
}
