﻿using NewsletterMVC.Data;
using NewsletterMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace NewsletterMVC.Controllers {
    public class NewsletterDatabaseConnectionHandler : IDisposable {
        public static NewsletterDatabaseConnectionHandler Instance { get; private set; }

        public static NewsletterDatabaseConnectionHandler GetOrCreate()
            => Instance ?? new NewsletterDatabaseConnectionHandler(
                @"Data Source=(localdb)\MSSQLLocalDB;" +
                @"Initial Catalog=Newsletter;" +
                @"Integrated Security=True;" +
                @"Connect Timeout=30;" +
                @"Encrypt=False;" +
                @"TrustServerCertificate=False;" +
                @"ApplicationIntent=ReadWrite;" +
                @"MultiSubnetFailover=False;"
        );

        public readonly SqlConnection Connection;

        private NewsletterDatabaseConnectionHandler(string connectionString) {
            Connection = new SqlConnection(connectionString);
            Connection.Open();
        }

        bool IsDisposed = false;
        public void Dispose() {
            if (IsDisposed) return;
            try { Connection.Dispose(); } catch { }
            GC.SuppressFinalize(this);
            IsDisposed = true;
        }

        ~NewsletterDatabaseConnectionHandler() => Dispose();
    }
    public class HomeController : Controller {
        public ActionResult Index() => View();
        [HttpPost]
        public ActionResult SignUp(string firstName, string lastName, string emailAddress) {
            bool firstNameValid = (firstName ?? "").Trim().Length > 2,
                lastNameValid = (lastName ?? "").Trim().Length > 2,
                emailAddressValid = Regex.IsMatch(emailAddress = emailAddress.Trim(), @"^[^\s@]+@[^\s@]+\.[^\s@]{2,}$");
            if (!firstNameValid || !lastNameValid || !emailAddressValid)
                return View("~/Views/Shared/Error.cshtml");

            SqlConnection connection = NewsletterDatabaseConnectionHandler.GetOrCreate().Connection;
            const string appendQuery = "INSERT INTO NewsletterSubscribers(FirstName, LastName, EmailAddress, SecretKey) " +
                "VALUES(@firstName, @lastName, @emailAddress, @secretKey);";
            SqlCommand cmd = new SqlCommand(appendQuery, connection);
            foreach (var parameter in new (string, SqlDbType, object)[] {
                    ("@firstName",     SqlDbType.VarChar, firstName ),
                    ("@lastName",      SqlDbType.VarChar, lastName ),
                    ("@emailAddress",  SqlDbType.VarChar, emailAddress),
                    ("@emailAddress",  SqlDbType.VarChar, emailAddress),
                    ("secretKey",      SqlDbType.VarChar, Guid.NewGuid().ToString().Substring(0, 16))
            }) {
                cmd.Parameters.Add(new SqlParameter(parameter.Item1, parameter.Item2) {
                    Value = parameter.Item3
                });
            }
            cmd.ExecuteNonQuery();
            return View(nameof(Success));
        }

        public ActionResult Admin() {
            SqlConnection connection = NewsletterDatabaseConnectionHandler.GetOrCreate().Connection;
            const string queryString = "SELECT * FROM NewsletterSubscribers";
            var entries = new List<PublicNewsletterSubscriberVm>();
            using (SqlCommand cmd = new SqlCommand(queryString, connection)) {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    entries.Add(new PublicNewsletterSubscriberVm(reader));
            }
            return View(entries);
        }

        public ActionResult Success() => View();

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}