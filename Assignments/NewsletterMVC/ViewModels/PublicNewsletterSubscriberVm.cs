using NewsletterMVC.Data;
using NewsletterMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NewsletterMVC.ViewModels {
    /// <summary>The object that has data, but not as sensitive</summary>
    public class PublicNewsletterSubscriberVm {
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public PublicNewsletterSubscriberVm(string firstName, string lastName, string emailAddress) {
            Id = -1;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
        }

        public PublicNewsletterSubscriberVm(SqlDataReader reader) {
            Id = reader.GetInt32(reader.GetOrdinal(nameof(Id)));
            FirstName = reader.GetString(reader.GetOrdinal(nameof(FirstName)));
            LastName = reader.GetString(reader.GetOrdinal(nameof(LastName)));
            EmailAddress = reader.GetString(reader.GetOrdinal(nameof(EmailAddress)));
        }

        public PublicNewsletterSubscriberVm(NewsletterSubscriber e)
            : this(e.FirstName, e.LastName, e.EmailAddress) {
            Id = e.Id;
        }
    }
}