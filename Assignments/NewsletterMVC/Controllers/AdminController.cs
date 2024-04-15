using NewsletterMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsletterMVC.Models;

namespace NewsletterMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index() {
            /*
            SqlConnection connection = NewsletterDatabaseConnectionHandler.GetOrCreate().Connection;
            const string queryString = "SELECT * FROM NewsletterSubscribers";
            var entries = new List<PublicNewsletterSubscriberVm>();
            using (SqlCommand cmd = new SqlCommand(queryString, connection)) {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    entries.Add(new PublicNewsletterSubscriberVm(reader));
            }
            return View(entries);*/

            // EF Implementation
            var subscribers = new List<PublicNewsletterSubscriberVm>();
            using (NewsletterEntities db = new NewsletterEntities()) {
                foreach (var subscriber in db.NewsletterSubscribers.Where(e => !e.RemovedDate.HasValue || DateTime.Compare(e.RemovedDate.Value, DateTime.Now) > 0))
                    subscribers.Add(new PublicNewsletterSubscriberVm(subscriber));
            }
            return View(subscribers);
        }

        [HttpPost] public ActionResult Unsubscribe(int subscriberId) {
            using (var db = new NewsletterEntities()) {
                var subscriber = db.NewsletterSubscribers.Find(subscriberId);
                if (subscriber.RemovedDate.HasValue && DateTime.Compare(subscriber.RemovedDate.Value, DateTime.Now) <= 0)
                    return RedirectToAction(nameof(Index));
                subscriber.RemovedDate = DateTime.Now;
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}