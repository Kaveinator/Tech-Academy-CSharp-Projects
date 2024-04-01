using System;

namespace AbstractClassAssignment {
    internal class Program {
        static void Main(string[] args) {
            new Employee(0, "Sample", "Student").SayName();

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

        public virtual void SayName() => Console.WriteLine($"{FirstName} {LastName}");
    }
    class Employee : Person {
        public readonly int Id;

        public Employee(int id, string firstName, string lastName)
            : base(firstName, lastName)
            => Id = id;

        public override void SayName() {
            Console.Write($"#{Id}: ");
            base.SayName();
        }
    }
}
