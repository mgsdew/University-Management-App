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
    public class CourseController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        //
        // GET: /Course/

        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.Department).Include(c => c.Semester);
            return View(courses.ToList());
        }

        //
        // GET: /Course/Details/5

        public ActionResult Details(int id = 0)
        {
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        //
        // GET: /Course/Create

        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name");
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "Name");
            Teacher aTeacher = new Teacher();
            aTeacher.Name = "";
            aTeacher.Email = "";
            ViewBag.teacher = aTeacher;
            return View();
        }

        //
        // POST: /Course/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                if(!db.Courses.Any(aCourse => course.Code == aCourse.Code) || !db.Courses.Any(aCourse => course.Name == aCourse.Name))
                {
                    db.Courses.Add(course);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", course.DepartmentId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "Name", course.SemesterId);
            return View(course);
        }

        //
        // GET: /Course/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", course.DepartmentId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "Name", course.SemesterId);
            return View(course);
        }

        //
        // POST: /Course/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", course.DepartmentId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "Name", course.SemesterId);
            return View(course);
        }

        //
        // GET: /Course/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        //
        // POST: /Course/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [HttpPost]
        public JsonResult IsCodeExist(string Code)
        {
            return Json(!db.Courses.Any(course => course.Code == Code), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IsNameExist(string Name)
        {
            return Json(!db.Courses.Any(course => course.Name == Name), JsonRequestBehavior.AllowGet);
        }


        public PartialViewResult SelectedDepartmentCourses(int? departmentId)
        {
            if (departmentId != null)
            {
                var departmentCourses = db.Courses.Where(c => c.DepartmentId == departmentId);
                ViewBag.CourseId = new SelectList(departmentCourses, "CourseId", "Code");

                return PartialView("~/Views/Shared/_DepartmentCourses.cshtml");
            }
            else
            {
                var departmentCourses = db.Courses.Where(c => c.DepartmentId == 0);
                ViewBag.CourseId = new SelectList(departmentCourses, "CourseId", "Code");
                return PartialView("~/Views/Shared/_DepartmentCourses.cshtml");
            }
        }
        public PartialViewResult SelectedStudentDepartmentCourses(int? studentId)
        {
            if (studentId != null)
            {
                var student = db.Students.Include(d=>d.Department).Where(s => s.StudentId == studentId).First();
                var departmentCourses = db.Courses.Where(c => c.DepartmentId == student.DepartmentId);
                ViewBag.CourseId = new SelectList(departmentCourses, "CourseId", "Code");

                return PartialView("~/Views/Shared/_DepartmentCourses.cshtml");
            }
            else
            {
                var departmentCourses = db.Courses.Where(c => c.DepartmentId == 0);
                ViewBag.CourseId = new SelectList(departmentCourses, "CourseId", "Code");
                return PartialView("~/Views/Shared/_DepartmentCourses.cshtml");
            }
        }

        public PartialViewResult SelectedStudentEnrollCourses(int? studentId)
        {
            if (studentId != null)
            {
                var encosids = db.EnrollCourses.Where(c => c.StudentId == studentId).Select(c => c.CourseId).ToList();
                var enrolledCourses = db.Courses.Where(c => encosids.Contains(c.CourseId));
                ViewBag.CourseId = new SelectList(enrolledCourses, "CourseId", "Code");

                return PartialView("~/Views/Shared/_DepartmentCourses.cshtml");
            }
            else
            {
                var departmentCourses = db.Courses.Where(c => c.DepartmentId == 0);
                ViewBag.CourseId = new SelectList(departmentCourses, "CourseId", "Code");
                return PartialView("~/Views/Shared/_DepartmentCourses.cshtml");
            }
        }

        public PartialViewResult ViewCourseInfo(int? courseId)
        {
            if (courseId != null)
            {
                var course = db.Courses.Where(c => c.CourseId == courseId).First();
                return PartialView("~/Views/Shared/_ViewCourseInfo.cshtml", course);
            }
            else
            {
                return PartialView("~/Views/Shared/_EmptyViewCourseInfo.cshtml");
            }
        }


    }
}