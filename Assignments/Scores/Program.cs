
namespace Scores {
    internal class Program {
        static void Main(string[] args) {
            Console.Write("First Name: ");
            string uName = Console.ReadLine(),
                date = DateTime.Today.ToShortDateString(),
                msg = $"\nWelcome back {uName}. Today is {date}";
            Console.WriteLine(msg);

            Console.WriteLine("\nStudent Scores:");
            string[] lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "studentScores.txt"));
            decimal tScore = 0m;
            int successfulIterations = 0;
            foreach (string line in lines) {
                if (decimal.TryParse(line, out decimal score)) {
                    Console.WriteLine($" - {score}");
                    tScore += score;
                    successfulIterations++;
                }
            }
            decimal avgScore = tScore / (decimal)successfulIterations;
            Console.WriteLine($"\nTotal of {successfulIterations} student scores." +
                $"\nAverage Score: {avgScore}" +
                $"\nFailed Iterations: {successfulIterations - lines.Count()}"
            );

            Console.WriteLine("\n\nPress any key to exit");
            _ = Console.ReadKey(true);
        }
    }
}
