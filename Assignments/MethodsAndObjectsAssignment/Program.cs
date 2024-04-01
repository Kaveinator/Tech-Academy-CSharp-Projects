using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodsAndObjects {
    internal class Program {
        static void Main(string[] args) {
            // Create a new Employee, but abstract it as Person, param names could also be used too
            Person employee = new Employee(id: 0, firstName: "Sample", lastName: "Student");
            employee.SayName();

            Console.ReadKey(true);
        }
    }
    public class Person {
        // Readonly makes the variable be able to be set only during object initilization
        public readonly string FirstName;
        public readonly string LastName;

        public Person(string firstName, string lastName) {
            FirstName = firstName;
            LastName = lastName;
        }

        public void SayName() => Console.WriteLine($"Name: {FirstName} {LastName}");
    }
    public class Employee : Person {
        public readonly int Id;

        public Employee(int id, string firstName, string lastName) : base(firstName, lastName) {
            Id = id;
        }
    }
}
