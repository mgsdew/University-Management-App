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
    public class ClassRoomController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        //
        // GET: /ClassRoom/

        public ActionResult Index()
        {
            return View(db.ClassRooms.ToList());
        }

        //
        // GET: /ClassRoom/Details/5

        public ActionResult Details(int id = 0)
        {
            ClassRoom classroom = db.ClassRooms.Find(id);
            if (classroom == null)
            {
                return HttpNotFound();
            }
            return View(classroom);
        }

        //
        // GET: /ClassRoom/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ClassRoom/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClassRoom classroom)
        {
            if (ModelState.IsValid)
            {
                db.ClassRooms.Add(classroom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(classroom);
        }

        //
        // GET: /ClassRoom/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ClassRoom classroom = db.ClassRooms.Find(id);
            if (classroom == null)
            {
                return HttpNotFound();
            }
            return View(classroom);
        }

        //
        // POST: /ClassRoom/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClassRoom classroom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classroom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(classroom);
        }

        //
        // GET: /ClassRoom/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ClassRoom classroom = db.ClassRooms.Find(id);
            if (classroom == null)
            {
                return HttpNotFound();
            }
            return View(classroom);
        }

        //
        // POST: /ClassRoom/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassRoom classroom = db.ClassRooms.Find(id);
            db.ClassRooms.Remove(classroom);
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