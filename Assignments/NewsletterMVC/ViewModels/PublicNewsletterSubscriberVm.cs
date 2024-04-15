using NewsletterMVC.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NewsletterMVC.ViewModels {
    /// <summary>The object that has data, but not as sensitive</summary>
    public class PublicNewsletterSubscriberVm {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public PublicNewsletterSubscriberVm(string firstName, string lastName, string emailAddress) {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
        }

        public PublicNewsletterSubscriberVm(SqlDataReader reader) {
            FirstName = reader.GetString(reader.GetOrdinal(nameof(FirstName)));
            LastName = reader.GetString(reader.GetOrdinal(nameof(LastName)));
            EmailAddress = reader.GetString(reader.GetOrdinal(nameof(EmailAddress)));
        }
    }
}