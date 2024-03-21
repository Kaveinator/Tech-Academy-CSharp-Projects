// Step 120
// Changing enviroment variables through control.exe will change it for admin
// Run "rundll32 sysdm.cpl,EditEnvironmentVariables" to open the dialog as current user
using System;

namespace SampleApplication {
  public static class Program {
    public static void Main(string[] args) {
      Console.WriteLine("Hello, World!");
    }
  }
}