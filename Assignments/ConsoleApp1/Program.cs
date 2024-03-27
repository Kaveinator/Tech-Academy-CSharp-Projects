using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1 {
    internal class Program {
        readonly static string[] Strings = new[] { // Req 252.1
            "Lorem ipsum dolor sit amet, ",
            "Ut enim ad minim veniam, ",
            "Duis aute irure dolor in ",
            "Excepteur sint occaecat cupidatat non proident, "
        };
        readonly static string[] SearchStrings = new[] { // Req 255.1
                "This is a delicious apple pie.",
                "I bought fresh apricots from the market.",
                "Banana smoothies are my favorite.",
                "Orange juice is packed with vitamin C.",
                "I love pineapple on my pizza.",
                "Grapes make a great snack.",
                "Strawberry ice cream is a classic dessert.",
                "Blueberry muffins are perfect for breakfast.",
                "Blackberry jam is tasty on toast.",
                "Raspberry sorbet is so refreshing.",
                "I ate a kiwi for the first time yesterday.",
                "Watermelon slices are perfect for summer.",
                "I enjoy eating melon salad.",
                "Peach cobbler is a Southern favorite.",
                "Pear and goat cheese make a delicious combination.",
                "Plum jam is delicious on croissants.",
                "Cherry pie is a classic American dessert.",
                "Mango salsa is great with grilled fish.",
                "Papaya smoothies are so refreshing.",
                "Lemon bars are tangy and sweet.",
                "Lime adds a zesty flavor to dishes.",
                "Fig preserves are a delicacy.",
                "Guava juice is popular in many tropical regions.",
                "Dragonfruit is exotic and visually stunning."
            };
        static void Main(string[] args) {
            // Uncomment a goto line to test a specific challenge
            //goto challenge2;
            //goto challenge3;
            //goto challenge4;
            //goto challenge5;
            //goto challenge6;

            // Step 252 : Challenge 1
            Console.WriteLine("# Part 1");
            Console.Write("Enter a string: "); // Req 252.2
            string appendedString = Console.ReadLine();
            for (int i = 0; i < Strings.Length; i++) // Req 252.3
                Strings[i] += appendedString;
            foreach (string sentence in Strings) // Req 252.4
                Console.WriteLine(sentence);

            // Step 253 : Challenge 2
        challenge2:
            Console.WriteLine("# Part 2");
            Console.WriteLine("Type exit to break out of the loop");
            while (true) { // Req 253.1
                // Req 253.4 : Infinite loop was fixed when I changed the word "Exit" to be all lowercase (since the incoming string was lower, compared was not
                if (Console.ReadLine().ToLower() == "exit") // Req 253.3
                    break;
            }

            // Step 254 : Challenge 3
        challenge3:
            Console.WriteLine("# Part 3");
            for (int i = 0; i < Strings.Length; i++) // Req 254.1
                Console.WriteLine(Strings[i]);
            for (int i = 1; i <= Strings.Length; i++) // Req 254.3
                Console.WriteLine(Strings[i - 1]);

            // Step 255 : Challenge 4 (uses SearchStrings array which fulfills Req 255.1)
        challenge4:
            Console.WriteLine("# Part 14");
            string searchQuery = "";
            do {
                Console.Write("Search query: ");  // Req 255.2
                searchQuery = Console.ReadLine();
            } while (string.IsNullOrEmpty(searchQuery));
            // Requirements 3 and 5 contridict each other, so I'll show different implementations
            IEnumerable<string> results = SearchStrings.Where<string>(str => str.Contains(searchQuery));
            int count = results.Count();
            Console.WriteLine($"Yielded {results.Count()} result(s)"); // Req 255.4
            foreach (string s in results) Console.WriteLine(s); // Req 255.3
            // Now how would I stop at first match?
            string result = SearchStrings.FirstOrDefault<string>(str => str.Contains(searchQuery));
            Console.WriteLine(string.IsNullOrEmpty(result) ? "No matches found" : $"Match found: {result}");

            // Step 256 : Challenge 5 (uses SearchStrings array which fulfills Req 256.1)
        challenge5:
            Console.WriteLine("# Part 5");
            searchQuery = "";
            do {
                Console.Write("New search query: ");
                searchQuery = Console.ReadLine();
            } while (string.IsNullOrEmpty(searchQuery));
            Regex regex = new Regex(searchQuery);
            results = SearchStrings.Where<string>(str => regex.Matches(str).Count > 1); // Req 256.2
            count = results.Count();
            Console.WriteLine($"Yielded {results.Count()} result(s)"); // Req 256.3: Displays 0 results if there were no matches
            foreach (string s in results) Console.WriteLine(s);

            // Challenge 6
        challenge6:
            Console.WriteLine("# Part 6");
            string[] findDupArr = new[] { "a", "b", "c", "d", "c" }; // Req 257.1
            for (int i = 0; i < findDupArr.Length; i++) // Iterate through the array
                for (int j = i - 1; j >= 0; j--) // At each step, backtrack
                    if (findDupArr[i] == findDupArr[j]) // If found another item behind current item
                        Console.WriteLine($"Found duplicate '{findDupArr[i]}' at index {i} (Last occurance: {j})");

            Console.ReadKey(true);
        }
    }
}
