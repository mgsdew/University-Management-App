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
    public class ResultGradeController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        //
        // GET: /ResultGrade/

        public ActionResult Index()
        {
            return View(db.ResultGrades.ToList());
        }

        //
        // GET: /ResultGrade/Details/5

        public ActionResult Details(int id = 0)
        {
            ResultGrade resultgrade = db.ResultGrades.Find(id);
            if (resultgrade == null)
            {
                return HttpNotFound();
            }
            return View(resultgrade);
        }

        //
        // GET: /ResultGrade/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ResultGrade/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ResultGrade resultgrade)
        {
            if (ModelState.IsValid)
            {
                db.ResultGrades.Add(resultgrade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resultgrade);
        }

        //
        // GET: /ResultGrade/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ResultGrade resultgrade = db.ResultGrades.Find(id);
            if (resultgrade == null)
            {
                return HttpNotFound();
            }
            return View(resultgrade);
        }

        //
        // POST: /ResultGrade/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ResultGrade resultgrade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resultgrade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resultgrade);
        }

        //
        // GET: /ResultGrade/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ResultGrade resultgrade = db.ResultGrades.Find(id);
            if (resultgrade == null)
            {
                return HttpNotFound();
            }
            return View(resultgrade);
        }

        //
        // POST: /ResultGrade/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ResultGrade resultgrade = db.ResultGrades.Find(id);
            db.ResultGrades.Remove(resultgrade);
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