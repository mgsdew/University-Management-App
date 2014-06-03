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
    public class EnrollCourseController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        //
        // GET: /EnrollCourse/

        public ActionResult Index()
        {
            var enrollcourses = db.EnrollCourses.Include(e => e.Student).Include(e => e.Course);
            return View(enrollcourses.ToList());
        }

        //
        // GET: /EnrollCourse/Details/5

        public ActionResult Details(int id = 0)
        {
            EnrollCourse enrollcourse = db.EnrollCourses.Find(id);
            if (enrollcourse == null)
            {
                return HttpNotFound();
            }
            return View(enrollcourse);
        }

        //
        // GET: /EnrollCourse/Create

        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "RegNo");
            ViewBag.CourseId = new SelectList(db.Courses.Where(c=> c.CourseId==0), "CourseId", "Code");
            return View();
        }

        //
        // POST: /EnrollCourse/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EnrollCourse enrollcourse)
        {
            if (ModelState.IsValid)
            {
                if (
                    db.EnrollCourses.Where(
                        e => e.StudentId == enrollcourse.StudentId && e.CourseId == enrollcourse.CourseId).Count() > 0)
                {
                    return RedirectToAction("Error");
                }
                db.EnrollCourses.Add(enrollcourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", enrollcourse.StudentId);
            ViewBag.CourseId = new SelectList(db.Courses.Where(c => c.CourseId == 0), "CourseId", "Code", enrollcourse.CourseId);
            return View(enrollcourse);
        }

        public ActionResult Error()
        {
            ViewBag.Message = "the student is already enrolled to this course.";
            return View();
        }

        //
        // GET: /EnrollCourse/Edit/5

        public ActionResult Edit(int id = 0)
        {
            EnrollCourse enrollcourse = db.EnrollCourses.Find(id);
            if (enrollcourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", enrollcourse.StudentId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", enrollcourse.CourseId);
            return View(enrollcourse);
        }

        //
        // POST: /EnrollCourse/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EnrollCourse enrollcourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollcourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", enrollcourse.StudentId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", enrollcourse.CourseId);
            return View(enrollcourse);
        }

        //
        // GET: /EnrollCourse/Delete/5

        public ActionResult Delete(int id = 0)
        {
            EnrollCourse enrollcourse = db.EnrollCourses.Find(id);
            if (enrollcourse == null)
            {
                return HttpNotFound();
            }
            return View(enrollcourse);
        }

        //
        // POST: /EnrollCourse/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EnrollCourse enrollcourse = db.EnrollCourses.Find(id);
            db.EnrollCourses.Remove(enrollcourse);
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