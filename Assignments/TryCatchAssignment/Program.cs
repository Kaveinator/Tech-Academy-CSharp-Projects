using System;
using static InputAssignment.ConsoleUtils;

namespace TryCatchAssignment {
    internal class Program {
        static void Main(string[] args) {
            try {
                byte age = ReadNumeral<byte>("Age:", 0, 100); // Read age between 0 and 100 (inclusive)
                if (age == 0) // If age is 0, throw an error
                    throw new AgeException("You can't be using a computer as a newborn! Where are your parents XD");
                if (age < 18)
                    throw new AgeException("You need a parent or guardian to fill out this form for you");
                Console.WriteLine($"You were born in {DateTime.Now.Year - age}"); // Show the year you were born
            } catch (AgeException e) {
                Console.WriteLine($"Age Restriction: {e.Message}");
            } catch (Exception e) {
                Console.WriteLine($"Internal Error: {e}");
            }

            while (true) _ = Console.ReadKey(true);
        }
    }
    public class AgeException : Exception {
        public AgeException(string msg) : base(msg) { }
    }
}
