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
    public class TeacherController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        //
        // GET: /Teacher/

        public ActionResult Index()
        {
            var teachers = db.Teachers.Include(t => t.Department).Include(t => t.Designation);
            return View(teachers.ToList());
        }

        //
        // GET: /Teacher/Details/5

        public ActionResult Details(int id = 0)
        {
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        //
        // GET: /Teacher/Create

        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name");
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "Name");
            return View();
        }

        //
        // POST: /Teacher/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                if (!db.Teachers.Any(t => t.Email == teacher.Email))
                {
                    db.Teachers.Add(teacher);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", teacher.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "Name", teacher.DesignationId);
            return View(teacher);
        }

        //
        // GET: /Teacher/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", teacher.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "Name", teacher.DesignationId);
            return View(teacher);
        }

        //
        // POST: /Teacher/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", teacher.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "Name", teacher.DesignationId);
            return View(teacher);
        }

        //
        // GET: /Teacher/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        //
        // POST: /Teacher/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult IsEmailExist(string Email)
        {
            return Json(!db.Teachers.Any(t => t.Email == Email), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult SelectedDepartmentTeachers(int? departmentId)
        {
            if (departmentId != null)
            {
                var departmentTeachers = db.Teachers.Where(c => c.DepartmentId == departmentId);
                ViewBag.TeacherId = new SelectList(departmentTeachers, "TeacherId", "Name");
                return PartialView("~/Views/Shared/_DepartmentTeachers.cshtml");
            }
            else
            {
                var departmentTeachers = db.Teachers.Where(c => c.DepartmentId == 0);
                ViewBag.TeacherId = new SelectList(departmentTeachers, "TeacherId", "Name");
                return PartialView("~/Views/Shared/_DepartmentTeachers.cshtml");
            }
        }

        public PartialViewResult ViewTeacherInfo(int? teacherId)
        {
            if (teacherId != null)
            {
                var teacher = db.Teachers.Where(d => d.TeacherId == teacherId).First();
                var assignedCredit = 0.0;
                try
                {
                    assignedCredit = db.CourseAssigns.Where(c => c.TeacherId == teacherId).Sum(c => c.Course.Credit);
                }
                catch (Exception e)
                {
                }

                ViewData["total_credit"] = teacher.TotalCredit;
                ViewData["remaining_credit"] = teacher.TotalCredit - assignedCredit;
                return PartialView("~/Views/Shared/_ViewTeacherInfo.cshtml");
            }
            else
            {
                return PartialView("~/Views/Shared/_EmptyViewTeacherInfo.cshtml");
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}