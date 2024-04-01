using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorsAssignment {
    internal class Program {
        static void Main(string[] args) {
            Employee a = new Employee(0, "Sample", "Student"),
                b = a, // Same student to test ==
                c = new Employee(1, "Another", "Student"); // Another student to test !=
            Console.WriteLine(a == b); // True
            Console.WriteLine(a == c); // False

            Console.ReadKey(true);
        }
    }
    abstract class Person {
        public readonly string FirstName;
        public readonly string LastName;

        public Person(string firstName, string lastName) {
            FirstName = firstName;
            LastName = lastName;
        }
    }
    class Employee : Person {
        public readonly int Id;

        public Employee(int id, string firstName, string lastName)
            : base(firstName, lastName)
            => Id = id;

        public static bool operator ==(Employee a, Employee b) => a.Id == b.Id;
        public static bool operator !=(Employee a, Employee b) => a.Id != b.Id;
    }
}
