using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.Controllers
{
    public class SemesterController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        //
        // GET: /Semester/

        public ActionResult Index()
        {
            return View(db.Semesters.ToList());
        }

        //
        // GET: /Semester/Details/5

        public ActionResult Details(int id = 0)
        {
            Semester semester = db.Semesters.Find(id);
            if (semester == null)
            {
                return HttpNotFound();
            }
            return View(semester);
        }

        //
        // GET: /Semester/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Semester/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Semester semester)
        {
            if (ModelState.IsValid)
            {
                db.Semesters.Add(semester);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(semester);
        }

        //
        // GET: /Semester/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Semester semester = db.Semesters.Find(id);
            if (semester == null)
            {
                return HttpNotFound();
            }
            return View(semester);
        }

        //
        // POST: /Semester/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Semester semester)
        {
            if (ModelState.IsValid)
            {
                db.Entry(semester).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(semester);
        }

        //
        // GET: /Semester/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Semester semester = db.Semesters.Find(id);
            if (semester == null)
            {
                return HttpNotFound();
            }
            return View(semester);
        }

        //
        // POST: /Semester/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Semester semester = db.Semesters.Find(id);
            db.Semesters.Remove(semester);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}