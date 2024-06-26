﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCodeFirstChallenge.Data;

namespace EntityFrameworkCodeFirstChallenge.Controllers {
    public class StudentController : Controller {
      static SchoolDatabaseContext Database => Program.SchoolDatabaseContext;

        public StudentController() { }

        // GET: Student
        public async Task<IActionResult> Index()
            => View(await Database.Students.ToListAsync());

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) return NotFound();

            var student = await Database.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null) return NotFound();

            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create() => View();

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName")] StudentController student) {
            if (ModelState.IsValid) {
                Database.Add(student);
                await Database.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) return NotFound();

            var student = await Database.Students.FindAsync(id);
            if (student == null) return NotFound();

            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName")] Student student) {
            if (id != student.Id) return NotFound();

            if (ModelState.IsValid) {
                try {
                    Database.Update(student);
                    await Database.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!StudentExists(student.Id))
                      return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var student = await Database.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
                return NotFound();

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var student = await Database.Students.FindAsync(id);
            if (student != null)
                Database.Students.Remove(student);

            await Database.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id) => Database.Students.Any(e => e.Id == id);
    }
}
