using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParametersAssignment {
    internal class Program {
        static void Main(string[] args) {
            Employee<string> employeeWithAString = new Employee<string>(0, "Sample", "Student");
            employeeWithAString.Things.Add("yarn");
            Employee<int> employeeWithANumeral = new Employee<int>(0, "Sample", "Student");
            employeeWithANumeral.Things.Add(1234);

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
    class Employee<T> : Person {
        public readonly int Id;
        public List<T> Things;

        public Employee(int id, string firstName, string lastName)
            : base(firstName, lastName) {
            Id = id;
            Things = new List<T>();
        }

        public override void SayName() {
            Console.Write($"#{Id}: ");
            base.SayName();
        }

        public void PrintThings() {
            foreach (T t in Things) Console.WriteLine(t.ToString());
        }
    }
}
