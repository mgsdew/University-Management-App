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
    public class WeekDayController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        //
        // GET: /WeekDay/

        public ActionResult Index()
        {
            return View(db.WeekDays.ToList());
        }

        //
        // GET: /WeekDay/Details/5

        public ActionResult Details(int id = 0)
        {
            WeekDay weekday = db.WeekDays.Find(id);
            if (weekday == null)
            {
                return HttpNotFound();
            }
            return View(weekday);
        }

        //
        // GET: /WeekDay/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /WeekDay/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WeekDay weekday)
        {
            if (ModelState.IsValid)
            {
                db.WeekDays.Add(weekday);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(weekday);
        }

        //
        // GET: /WeekDay/Edit/5

        public ActionResult Edit(int id = 0)
        {
            WeekDay weekday = db.WeekDays.Find(id);
            if (weekday == null)
            {
                return HttpNotFound();
            }
            return View(weekday);
        }

        //
        // POST: /WeekDay/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WeekDay weekday)
        {
            if (ModelState.IsValid)
            {
                db.Entry(weekday).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(weekday);
        }

        //
        // GET: /WeekDay/Delete/5

        public ActionResult Delete(int id = 0)
        {
            WeekDay weekday = db.WeekDays.Find(id);
            if (weekday == null)
            {
                return HttpNotFound();
            }
            return View(weekday);
        }

        //
        // POST: /WeekDay/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WeekDay weekday = db.WeekDays.Find(id);
            db.WeekDays.Remove(weekday);
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