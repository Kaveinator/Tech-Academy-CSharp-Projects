using NewsletterMVC.ViewModels;
using System;
using System.Data.SqlClient;

namespace NewsletterMVC.Data {
    /// <summary>Only to be used internally, contains sensitive subscriber information</summary>
    internal class NewsletterSubscriberEntity : PublicNewsletterSubscriberVm {
        // This had the secret key and Id of the subscriber, but since those got removed this class is essentially useless

        public NewsletterSubscriberEntity(string firstName, string lastName, string emailAddress, string secretKey = null)
            : base(firstName, lastName, emailAddress) {

        }

        public NewsletterSubscriberEntity(SqlDataReader reader) : base(reader) {

        }
    }
}
