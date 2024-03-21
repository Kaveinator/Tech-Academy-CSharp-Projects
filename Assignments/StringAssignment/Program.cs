using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringAssignment {
    internal class Program {
        static void Main(string[] args) {
            string str1 = "Hello",
                str2 = "World",
                str3 = "!";
            string concatedString = $"{str1} {str2}{str3}".ToUpper(); // This concats three strings into one and capitalizes it
            StringBuilder sb = new StringBuilder();
            // The loop appends to the stringbuilder, one line at a time
            foreach (string str in new[] {
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
            }) sb.AppendLine(str);
        }
    }
}
