﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Assignments.ConsoleUtil;

namespace Assignments {
    /// <summary>
    /// A project I made to help with grading to TTA, and make all short,
    /// assignments be in a single project. Unfortunatly this was rejected
    /// and got lectured that I wasn't supposed to do that go figure
    /// </summary>
    public class Program {
        internal static readonly List<IAssignment> Assignments;
        
        static Program() {
            // Index all assignments
            Assignments = new List<IAssignment>(
                AppDomain.CurrentDomain.GetAssemblies() // Get all projects
                .SelectMany(assembly => assembly.GetTypes()) // Select many, to select multiple types per assembly
                .Where(type => typeof(IAssignment).IsAssignableFrom(type) && type != typeof(IAssignment)) // Check if the type implements IAssingment and ensure its not the IAssignment interface itself
                .Select(type => (IAssignment)Activator.CreateInstance(type)) // Create an instance of each type and cast to IAssignment
                .OrderByDescending(assignment => assignment.Step) // Order the assignments so that the latest assignment appears on top
            );
        }

        static void Main(string[] args) {
            // Build assignments string, its type if dynamic because at first its a stringbuilder, then a string after it has been built
            dynamic assignmentsListings = new StringBuilder("Select an Assignment to Review");
            ushort i = 0;
            assignmentsListings.AppendFormat("\n  {0}: {1}", i, "Exit");
            while (i < Assignments.Count) {
                IAssignment assignment = Assignments[i];
                assignmentsListings.AppendFormat("\n  {0}: Step {1}: {2}", ++i, assignment.Step, assignment.Name);
            }
            assignmentsListings.AppendLine();
            assignmentsListings = assignmentsListings.ToString(); // Now assignmentsListing is a string and will be used as such
            // The loop breaks when exit is selected (index 0)
            while (true) {
                //TestReadNumeral();
                Console.Clear(); // Clear out any previous output
                Console.Title = "Select Assignment";
                Console.WriteLine(assignmentsListings);

                ushort selectedIndex = ReadNumeral<ushort>("Index", 0, (ushort)Assignments.Count);
                if (selectedIndex-- == 0) break; // If selected index is 0, break, this line also subtracts one from selectedIndex so it can be used to get an array item

                Console.Clear(); // Clear console to run an assignment
                Run(Assignments[selectedIndex]); // Run said assignment
                Console.Write("\nPress any key to list all assignments...");
                _ = Console.ReadKey(intercept: true); // After the assignment finishes, wait for a keypress before looping back, the intercept flag prevents the key from being visible in the console
            }
        }

        static void Run<TAssignment>(TAssignment assignment) where TAssignment : IAssignment {
            Console.Title = $"Step {assignment.Step}: {assignment.Name}";
            assignment.Invoke();
        }

        // These two methods don't really serve a purpose in this case
        public static void Run<TAssignment>(Func<TAssignment> ctor) where TAssignment: IAssignment => Run<TAssignment>(ctor());
        public static void Run<TAssignment>() where TAssignment : IAssignment, new() => Run<TAssignment>(new TAssignment());

        static void TestReadNumeral() {
            Console.Clear();
            Console.Title = "Testing Numeral Input Parser";
            bool boolTest = ReadNumeral<bool>("bool", maxValue: true);
            byte byteTest = ReadNumeral<byte>("byte", 0, 100);
            ushort ushortTest = ReadNumeral<ushort>("ushort", 0, 100);
            uint uintTest = ReadNumeral<uint>("uint", 0, 100);
            ulong ulongTest = ReadNumeral<ulong>("ulong", 0, 100);
            Console.Write('\n');
            sbyte sbyteTest = ReadNumeral<sbyte>("sbyte", -100, 100);
            short shortTest = ReadNumeral<short>("short", 0, 100);
            int intTest = ReadNumeral<int>("int", 0, 100);
            long longTest = ReadNumeral<long>("long", 0, 100);

            Console.WriteLine(
                $"# Test Results" +
                $"\nbool:   {boolTest}" +
                $"\nbyte:   {byteTest}" +
                $"\nushort: {ushortTest}" +
                $"\nuint:   {uintTest}" +
                $"\nulong:  {ulongTest}" +
                $"\n" +
                $"\nsbyte:  {sbyteTest}" +
                $"\nshort:  {shortTest}" +
                $"\nint:    {intTest}" +
                $"\nlong:   {longTest}" +
                $"\n" +
                $"\nDone, press any key to exit"
            );
            Console.ReadKey();
        }
    }

    public interface IAssignment {
        ushort Step { get; }
        string Name { get; }
        void Invoke();
    }
}
