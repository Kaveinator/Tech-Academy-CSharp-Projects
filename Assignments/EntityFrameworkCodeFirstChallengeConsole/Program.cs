using System;
using EntityFrameworkCodeFirstChallengeConsole.Data;

namespace EntityFrameworkCodeFirstChallengeConsole {
    internal class Program {
        static void Main(string[] args) {
            using (var ctx = new SchoolContext()) {
                var stud = new Student() {
                    FirstName = "John",
                    LastName = "Doe"
                };

                ctx.Students.Add(stud);
                ctx.SaveChanges();
            }

            Console.WriteLine("Done");
            while (true) _ = Console.ReadKey(true);
        }
    }
}
