using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechAcadStudentsMVC.Models;

namespace TechAcadStudentsMVC.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Contact Page - The Tech Academy";

            return View();
        }
        static List<Instructor> InstructorsDir = new List<Instructor>(new[] {
            new Instructor(1, "Erik", "Gross"),
            new Instructor(2, "John", "Doe"),
            new Instructor(3, "John", "Smith")
        });
        static Instructor InvalidInstructor = new Instructor(-1, "Invalid", "Invalid");

        public ActionResult Instructors() => View(InstructorsDir);

        public ActionResult Instructor(int id) {
            Instructor instructor = InstructorsDir.FirstOrDefault(i => i.Id == id);
            return instructor != null ? View(instructor) : (ActionResult)RedirectToAction(nameof(Instructors));
        }
    }
}