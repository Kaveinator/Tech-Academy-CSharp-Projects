using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarInsurance.Models;

namespace CarInsurance.Controllers {
    public class InsureeController : Controller {
        private InsuranceEntities db = new InsuranceEntities();

        // GET: Insuree
        public ActionResult Index() => View(db.Insurees.ToList());

        // GET: Insuree/Details/5
        public ActionResult Details(int? id) {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
                return HttpNotFound();
            return View(insuree);
        }

        // GET: Insuree/Create
        public ActionResult Create() => View();

        // POST: Insuree/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,Lastname,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType")] Insuree insuree) {
            if (ModelState.IsValid) {
                insuree.Quote = 50;// Req 517.1a: Base 50/m
                int age = DateTime.Now.Year - insuree.DateOfBirth.Year;
                insuree.Quote += age <= 18 ? 100 // Req 517.1b: 18 and under get +100/m
                    : age <= 25 ? 50 : 25; // Req 517.1c, 517.1d: 19-25 get +50/m, 26+ get +25/m
                insuree.Quote += insuree.CarYear < 2000 || 2015 < insuree.CarYear // Req 517.1e, 517.1f: Add 25/m if older than 2000 and newer than 2015
                    ? 25 : 0;
                // Req 517.1g, 517.1h: +25/m for Porshe make, +50/m for Porshe 911 Carrera
                insuree.Quote += insuree.CarMake.Equals("Porsche", StringComparison.CurrentCultureIgnoreCase)
                    ? insuree.CarModel.Equals("911 Carrera", StringComparison.CurrentCultureIgnoreCase)
                    ? 50 : 25 : 0;
                insuree.Quote += Math.Max(insuree.SpeedingTickets, 0) * 10; // Req 517.1i: +10/m per ticket
                decimal multiplier = 1;
                insuree.Quote *= insuree.DUI ? 1.25m : 1m; // Req 517.1j: if DUI, add 25%
                insuree.Quote *= insuree.CoverageType ? 1.50m : 1m; // Req 517.1j: if Full Coverage, add 50%
                insuree.Quote *= multiplier;
                db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction(nameof(AdminController.Index), "Admin");
            }

            return View(insuree);
        }

        // GET: Insuree/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
                return HttpNotFound();
            return View(insuree);
        }

        // POST: Insuree/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,Lastname,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree) {
            if (ModelState.IsValid) {
                db.Entry(insuree).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(nameof(AdminController.Index), nameof(AdminController));
            }
            return View(insuree);
        }

        // GET: Insuree/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
                return HttpNotFound();
            return View(insuree);
        }

        // POST: Insuree/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Insuree insuree = db.Insurees.Find(id);
            db.Insurees.Remove(insuree);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
