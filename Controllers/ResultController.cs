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
    public class ResultController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        //
        // GET: /Result/

        public ActionResult Index()
        {
            //var results = db.Results.Include(r => r.Student).Include(r => r.Course).Include(r => r.ResultGrade);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "RegNo");
            
            return View();
        }

        //
        // GET: /Result/Details/5

        public ActionResult Details(int id = 0)
        {
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        //
        // GET: /Result/Create

        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "RegNo");
            ViewBag.CourseId = new SelectList(db.Courses.Where(c => c.CourseId == 0), "CourseId", "Code");
            ViewBag.ResultGradeId = new SelectList(db.ResultGrades, "ResultGradeId", "GradeLater");
            return View();
        }

        //
        // POST: /Result/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Result result)
        {
            if (ModelState.IsValid)
            {
                db.Results.Add(result);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ResultGradeId = new SelectList(db.ResultGrades, "ResultGradeId", "GradeLater", result.ResultGradeId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "RegNo");
            ViewBag.CourseId = new SelectList(db.Courses.Where(c => c.CourseId == 0), "CourseId", "Code");
            return View(result);
        }

        //
        // GET: /Result/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", result.StudentId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", result.CourseId);
            ViewBag.ResultGradeId = new SelectList(db.ResultGrades, "ResultGradeId", "GradeLater", result.ResultGradeId);
            return View(result);
        }

        //
        // POST: /Result/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Result result)
        {
            if (ModelState.IsValid)
            {
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", result.StudentId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", result.CourseId);
            ViewBag.ResultGradeId = new SelectList(db.ResultGrades, "ResultGradeId", "GradeLater", result.ResultGradeId);
            return View(result);
        }

        //
        // GET: /Result/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        //
        // POST: /Result/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Result result = db.Results.Find(id);
            db.Results.Remove(result);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public PartialViewResult ViewStudentResult(int? studentId)
        {
            if (studentId != null)
            {
                var result = db.Results.Include(r=>r.Course).Include(r=>r.ResultGrade).Where(c => c.StudentId == studentId);
                return PartialView("~/Views/Shared/_ViewResult.cshtml", result);
            }
            else
            {
                return PartialView("~/Views/Shared/_EmptyResultView.cshtml");
            }
        }
    }
}