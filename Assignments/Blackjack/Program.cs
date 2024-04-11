using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using static Blackjack.ConsoleUtils;
using System.Data.SqlClient;
using System.Data;

namespace Blackjack {
    internal static class Program {
        public static Blackjack CurrentGame;
        public static LogFile Logger;
        static SqlConnection ErrorReportingConnection;

        static void Main(string[] args) {
            using (SqlConnection connection = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;" +
                @"Initial Catalog=Blackjack;" +
                @"Integrated Security=True;" +
                @"Connect Timeout=30;" +
                @"Encrypt=False;" +
                @"TrustServerCertificate=False;" +
                @"ApplicationIntent=ReadWrite;" +
                @"MultiSubnetFailover=False;"
            )) {
                connection.Open();
                ErrorReportingConnection = connection;
                AppDomain.CurrentDomain.UnhandledException += (sender, e) => {
                    if (e.ExceptionObject is Exception ex) ReportError(ex);
                };
                Logger = new LogFile();
                Console.Write("# Welcome to the Grand Hotel\nWhats your name: ");
                string playerName = Console.ReadLine();
                if (playerName.Equals("admin", StringComparison.InvariantCultureIgnoreCase)) {
                    List<ExceptionEntity> exceptions = ReadExceptions();
                    if (exceptions.Count > 0) {
                        Console.WriteLine(string.Format("| {0,4} | {1,-18} | {2,-30} | {3,-50} |", "Id", "Timestamp", "Type", "Message"));
                        foreach (ExceptionEntity e in exceptions)
                            Console.WriteLine(string.Format(
                                "| {0,4} | {1,18} | {2,-30} | {3,-50} |",
                                e.Id, e.Timestamp, e.Type,
                                e.Message.Length <= 50 ? e.Message : $"{e.Message.Substring(0, 47)}..."
                            )
                        );
                    }
                    else Console.WriteLine("No errors have been reported");
                    goto end;
                }
                int balance = ReadNumeral<int>("How much money are you betting today?", minValue: 0, maxValue: 100);
                BoolResponse play = ReadEnum<BoolResponse>("Up for a round of blackjack?");
                if (play.AsBool()) {
                    BlackjackPlayer player = new BlackjackPlayer(playerName, balance);
                    CurrentGame = new Blackjack(Logger);
                    CurrentGame += player;
                    while (player.IsActivelyPlaying && player.Balance > 0) {
                        CurrentGame.Play();
                    }
                    CurrentGame -= player;
                    Console.WriteLine("Thanks for playing!");
                }
                Console.WriteLine("Feel free to look around, see you around");
            }
            end:
            while (true) _ = Console.ReadKey(true);
        }

        public static void ReportError(Exception e) {
            const string appendQuery = "INSERT INTO Exceptions(Type, Message, Timestamp) VALUES " +
                "(@type, @message, @timestamp);";
            SqlCommand cmd = new SqlCommand(appendQuery, ErrorReportingConnection);
            foreach (var parameter in new (string, SqlDbType, object)[] {
                    ("@type",       SqlDbType.VarChar,  e.GetType().FullName ),
                    ("@message",    SqlDbType.VarChar,  e.Message ),
                    ("@timestamp",  SqlDbType.DateTime, DateTime.Now)
                }) {
                cmd.Parameters.Add(new SqlParameter(parameter.Item1, parameter.Item2) {
                    Value = parameter.Item3
                });
            }
            cmd.ExecuteNonQuery();
        }

        public static List<ExceptionEntity> ReadExceptions() {
            const string selectQuery = "SELECT * FROM Exceptions;";
            List<ExceptionEntity> entities = new List<ExceptionEntity>();
            SqlCommand cmd = new SqlCommand(selectQuery, ErrorReportingConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                entities.Add(new ExceptionEntity(reader));
            return entities;
        }
    }
    public enum BoolResponse : sbyte { 
        No = sbyte.MinValue,
        Nope,
        Nah,

        Yep = 1,
        Yeah,
        Yes = sbyte.MaxValue
    }
    public static class BoolResponseExt {
        public static bool AsBool(this BoolResponse response) => response > 0;
    }
}
