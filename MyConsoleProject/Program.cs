using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsoleProject {
    internal class Program {
        static void Main() {
            Console.Write("What is your name? ");
            string name = Console.ReadLine();
            Console.WriteLine($"Hello, {name}!");
            Console.WriteLine("Welcome to Acme Accounting Systems\nRememeber, we're \"accounting\" on you!");
            Console.Read();
        }
    }
}
