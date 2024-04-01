using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolymorphismAssignment {
    internal class Program {
        static void Main(string[] args) {
            Person person = new Employee(0, "Sample", "Student");
            if (person is IQuittable quitabledPerson) {
                quitabledPerson.Quit();
            }
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
    class Employee : Person, IQuittable {
        public readonly int Id;

        public Employee(int id, string firstName, string lastName)
            : base(firstName, lastName)
            => Id = id;

        public override void SayName() {
            Console.Write($"#{Id}: ");
            base.SayName();
        }

        public void Quit() => Console.WriteLine($"Employee #{Id} Quit");
    }
    interface IQuittable {
        void Quit();
    }
}
