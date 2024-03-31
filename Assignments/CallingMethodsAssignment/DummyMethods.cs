using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallingMethodsAssignment {
    internal class DummyMethods {
        public static int Method1(int i) => i ^ 1;
        public static int Method2(int i) => Method1(i) ^ 2;
        public static int Method3(int i) => Method2(i) ^ 3;
    }
}
