using System;

namespace MainMethodAssignment {
    internal class Program {
        static void Main(string[] args) {
            DummyMethods obj1 = new DummyMethods(2),
                obj2 = new DummyMethods(3),
                obj3 = new DummyMethods(4); // Not entirely sure _why_ we have to create three instances, but alright
            Console.WriteLine(
                "# Tests" +
                $"\n  1: {obj1.Method1(123)}" +
                $"\n  2: {obj1.Method1(123m)}" +
                $"\n  3: {obj1.Method1("123")}"
            );
            Console.ReadKey(true);
        }
    }
    internal class DummyMethods {
        public readonly int Base;
        public DummyMethods(int baseInt) => Base = baseInt;
        // These are just random math stuff, doesn't do anything specific
        public int Method1(int i) => i ^ Base;
        public int Method1(decimal i) => (int)(i * Base);
        public int Method1(string i) => int.TryParse(i, out int o) ? Method1(o) * 2 : 0;
    }
}
