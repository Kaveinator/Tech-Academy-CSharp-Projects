using System;
using System.IO;

namespace Blackjack {
    public class LogFile : IDisposable {
        public StreamWriter Stream { get; private set; }

        public LogFile(string fileName = "latest.log") {
            string filePath = Path.Combine(Environment.CurrentDirectory, fileName);
            Stream = new StreamWriter(filePath, append: true);
        }

        public object Log(object msg) {
            Stream.WriteLine($"[{DateTime.Now}] {msg}");
            Stream.Flush();
            return msg;
        }

        public void Dispose() {
            if (Stream != null) {
                Stream.Dispose();
                Stream = null;
            }
            GC.SuppressFinalize(this);
        }

        ~LogFile() => Dispose();
    }
}
