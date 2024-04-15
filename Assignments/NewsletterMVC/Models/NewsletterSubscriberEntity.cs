using NewsletterMVC.ViewModels;
using System;
using System.Data.SqlClient;

namespace NewsletterMVC.Data {
    /// <summary>Only to be used internally, contains sensitive subscriber information</summary>
    internal class NewsletterSubscriberEntity : PublicNewsletterSubscriberVm {
        public int Id { get; private set; }
        public string SecretKey { get; set; }

        public NewsletterSubscriberEntity(string firstName, string lastName, string emailAddress, string secretKey = null)
            : base(firstName, lastName, emailAddress) {
            Id = -1; // Meaning "Not in DB"
            SecretKey = secretKey ?? Guid.NewGuid().ToString().Substring(0, 16);
        }

        public NewsletterSubscriberEntity(SqlDataReader reader) : base(reader) {
            Id = reader.GetInt32(reader.GetOrdinal(nameof(Id)));
            SecretKey = reader.GetString(reader.GetOrdinal(nameof(SecretKey)));
        }
    }
}
