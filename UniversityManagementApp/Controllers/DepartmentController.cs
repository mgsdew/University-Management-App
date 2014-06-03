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
    public class DepartmentController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        //
        // GET: /Department/

        public ActionResult Index()
        {
            return View(db.Departments.ToList());
        }

        //
        // GET: /Department/Details/5

        public ActionResult Details(int id = 0)
        {
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        //
        // GET: /Department/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Department/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                if (department.Code==""||department.Name==""||
                    db.Departments.Any(aDepartment => aDepartment.Name == department.Name) ||
                    db.Departments.Any(aDepartment => aDepartment.Code == department.Code))
                {
                    return RedirectToAction("Index");
                }
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(department);
        }

        //
        // GET: /Department/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        //
        // POST: /Department/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        //
        // GET: /Department/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        //
        // POST: /Department/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        [HttpPost]
        public JsonResult doesDepartmentNameExist(string Name)
        {
            return Json(!db.Departments.Any(department => department.Name == Name), JsonRequestBehavior.AllowGet);
        }

      
        [HttpPost]
        public JsonResult doesDepartmentCodeExist(string Code)
        {
            return Json(!db.Departments.Any(department => department.Name == Code), JsonRequestBehavior.AllowGet);
        }
    }
}