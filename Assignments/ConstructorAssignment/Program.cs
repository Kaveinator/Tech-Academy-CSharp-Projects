using System;

namespace ConstructorAssignment {
    internal class Program {
        static void Main(string[] args) {
            var user = new User("User1");
            user = new User(new Guid(), "Zero'd out"); // Create users
        }
    }
    public class User {
        public readonly Guid Id;
        public string DisplayName { get; protected set; }

        public User(string displayName) : this(Guid.NewGuid(), displayName) { }

        public User(Guid id, string displayName) {
            Id = id;
            DisplayName = displayName;
        }
    }
}
