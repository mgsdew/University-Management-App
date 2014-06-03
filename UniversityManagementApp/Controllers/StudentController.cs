using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.Controllers
{
    public class StudentController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        //
        // GET: /Students/

        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Department);
            return View(students.ToList());
        }

        //
        // GET: /Students/Details/5

        public ActionResult Details(int id = 0)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //
        // GET: /Students/Create

        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name");
            return View();
        }

        //
        // POST: /Students/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                student.RegNo = GenerateRegNo(student);
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = student.StudentId });
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", student.DepartmentId);
            return View(student);
        }

        //
        // GET: /Students/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", student.DepartmentId);
            return View(student);
        }

        //
        // POST: /Students/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", student.DepartmentId);
            return View(student);
        }

        //
        // GET: /Students/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //
        // POST: /Students/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public PartialViewResult ViewStudentInfo(int? studentId)
        {
            if (studentId != null)
            {
                var student = db.Students.Include(d => d.Department).Where(s => s.StudentId == studentId).First();
                return PartialView("~/Views/Shared/_ViewStudentInfo.cshtml", student);
            }
            else
            {
                var enrollCourse = new EnrollCourse();
                return PartialView("~/Views/Shared/_EmptyViewStudentinfo.cshtml");
            }
        }

        public string GenerateRegNo(Student student)
        {
            var departmentName =
                db.Departments.Where(d => d.DepartmentId == student.DepartmentId).Select(d => d.Code).First();
            var year = student.RegistrationDate.Year;
            var roll = "0000" + (db.Students.Where(s => s.RegistrationDate.Year == year && s.DepartmentId == student.DepartmentId).Count() + 1);

            return (departmentName.Length > 3 ? departmentName.Substring(0, 3) : departmentName )+ year + roll.Substring(roll.Length - 3);
        }
    }
}