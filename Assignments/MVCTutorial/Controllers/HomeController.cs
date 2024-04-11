using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using FileIO = System.IO.File;
using MVCTutorial.Models;

namespace MVCTutorial.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            User user = new User() {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Age = 22
            };

            return View(user);
        }

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