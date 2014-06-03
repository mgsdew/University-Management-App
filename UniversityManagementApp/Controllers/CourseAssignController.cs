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
    public class CourseAssignController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        //
        // GET: /CourseAssign/

        public ActionResult Index()
        {
            var courseassigns = db.CourseAssigns.Include(c => c.Teacher).Include(c => c.Course);
            return View(courseassigns.ToList());
        }

        //
        // GET: /CourseAssign/Details/5

        public ActionResult Details(int id = 0)
        {
            CourseAssign courseassign = db.CourseAssigns.Find(id);
            if (courseassign == null)
            {
                return HttpNotFound();
            }
            return View(courseassign);
        }

        //
        // GET: /CourseAssign/Create

        public ActionResult Create(int? departmentId)
        {
            if (departmentId == null)
            {
                ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name");
                ViewBag.TeacherId = new SelectList(db.Teachers.Where(t=> t.TeacherId == 0), "TeacherId", "Name");
                ViewBag.CourseId = new SelectList(db.Courses.Where(c=>c.CourseId == 0), "CourseId", "Code");
                return View();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name");
            ViewBag.TeacherId = new SelectList(db.Teachers.Where(t => t.DepartmentId == departmentId), "TeacherId", "Name");
            ViewBag.CourseId = new SelectList(db.Courses.Where(c=> c.DepartmentId == departmentId), "CourseId", "Code");
            return View();
        }

        //
        // POST: /CourseAssign/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseAssign courseassign,string Command, int? DepartmentId)
        {
            if (Command == "Select")
            {
                return Create(DepartmentId);
            }
            if (ModelState.IsValid)
            {
                if (courseassign.TeacherId == null || courseassign.CourseId==null)
                {
                    ViewBag.Message = "Error: All fields are required.";
                    return Create(null);
                }
                if (db.CourseAssigns.Any(c => c.CourseId == courseassign.CourseId))
                {
                    ViewBag.Message = "Error: The Course is already assigned to another teacher.";
                    return Create(null);
                }
                
                double totalCredit = (double)db.Teachers.Where(t=>t.TeacherId == courseassign.TeacherId).Select(t=>t.TotalCredit).First();
                double courseCredit = (double) db.Courses.Where(c => c.CourseId == courseassign.CourseId).Select(c=> c.Credit).First();
                double assignedCredit = 0.0;
                try
                {
                    assignedCredit = (double)db.CourseAssigns.Where(c => c.TeacherId == courseassign.TeacherId).Sum(c => c.Course.Credit);
                }
                catch (Exception ex) { }

                if (totalCredit < (courseCredit + assignedCredit))
                {
                   return RedirectToAction("CreditOverFlow",courseassign);
                }
                db.CourseAssigns.Add(courseassign);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name");
            ViewBag.TeacherId = new SelectList(db.Teachers.Where(t => t.TeacherId == 0), "TeacherId", "Name");
            ViewBag.CourseId = new SelectList(db.Courses.Where(c => c.CourseId == 0), "CourseId", "Code");
            return View(courseassign);
        }

        public ActionResult CreditOverFlow(CourseAssign courseassign)
        {
            ViewBag.Message = "Selected Teacher does not have enough Credit.\n Do you Still want to continue?";
            return View(courseassign);
        }

        [HttpPost, ActionName("CreditOverFlow")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateConfirmed(CourseAssign courseassign)
        {
            db.CourseAssigns.Add(courseassign);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /CourseAssign/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CourseAssign courseassign = db.CourseAssigns.Find(id);
            if (courseassign == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name", courseassign.TeacherId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", courseassign.CourseId);
            return View(courseassign);
        }

        //
        // POST: /CourseAssign/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CourseAssign courseassign)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseassign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name", courseassign.TeacherId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", courseassign.CourseId);
            return View(courseassign);
        }

        //
        // GET: /CourseAssign/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CourseAssign courseassign = db.CourseAssigns.Find(id);
            if (courseassign == null)
            {
                return HttpNotFound();
            }
            return View(courseassign);
        }

        //
        // POST: /CourseAssign/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseAssign courseassign = db.CourseAssigns.Find(id);
            db.CourseAssigns.Remove(courseassign);
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