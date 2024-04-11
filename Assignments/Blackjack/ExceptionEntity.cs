using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack {
    public class ExceptionEntity {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }

        public ExceptionEntity(Exception e) {
            Id = -1; // Not in DB
            Type = e.GetType().FullName;
            Message = e.Message;
            Timestamp = DateTime.Now;
        }

        public ExceptionEntity(SqlDataReader reader) {
            Id = reader.GetInt32(reader.GetOrdinal(nameof(Id)));
            Type = reader.GetString(reader.GetOrdinal(nameof(Type)));
            Message = reader.GetString(reader.GetOrdinal(nameof(Message)));
            Timestamp = reader.GetDateTime(reader.GetOrdinal(nameof(Timestamp)));
        }

    }
}
