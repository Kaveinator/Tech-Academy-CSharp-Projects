using System;

namespace Assignments {
    public class HelloWorld : IAssignment {
        ushort IAssignment.Step => 140;
        string IAssignment.Name => "Intro Assignment/Hello World";

        public void Invoke() {
            Console.WriteLine("Hello, World!");
        }
    }
}
