using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaAssignment {
    internal class Program {
        static void Main(string[] args) {
            byte counter = 0;
            // Create employee collection
            List<Employee> employees = new List<Employee>(
                new[] { 
                    "Joe", "Joe", "Jane", "John", "Mary",
                    "Bob", "David", "Jack", "Kevin, Josh"
                }.Select(firstName => new Employee(counter++, firstName, "Doe"))
            );
            // Foreach all names that are Joe
            foreach (Employee employee in employees)
                if (employee.FirstName == "Joe")
                    employee.SayName();

            // Linq all names that are Joe
            employees.Where(e => e.FirstName == "Joe").Do(e => e.SayName());

            _ = employees.Where(e => e.Id > 5); // Id greater than 5

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
    public static class Extensions {
        public static void Do<T>(this IEnumerable<T> source, Action<T> action) {   // note: omitted arg/null checks
            foreach (T item in source) { action(item); }
        }
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) {   // note: omitted arg/null checks
            foreach (T item in source) { action(item); }
        }
    }
}
