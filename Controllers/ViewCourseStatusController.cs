using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.Controllers
{
    public class ViewCourseStatusController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();
        //
        // GET: /ViewCourseStatus/

        public ViewResult Index(int? departmentId)
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name");
            List<ViewCourseStatus> filteredViews = new List<ViewCourseStatus>();
            var courses = db.Courses.Where(c => c.DepartmentId != 0);
            var selectedViews = db.CourseAssigns.Include(c => c.Teacher).Include(c => c.Course);
            if (departmentId != null)
            {
                courses = db.Courses.Where(c => c.DepartmentId == departmentId);
                selectedViews = db.CourseAssigns.Include(c => c.Teacher).Include(c => c.Course).Where(c => (c.Course.DepartmentId == departmentId));
            }


            foreach (var course in courses)
            {
                ViewCourseStatus aViewStatus = new ViewCourseStatus();
                if (db.CourseAssigns.Any(c => c.CourseId == course.CourseId))
                {
                    aViewStatus.Code = course.Code;
                    aViewStatus.Name = course.Name;
                    var teacherName = selectedViews.Include(c => c.Teacher).Where(c => c.CourseId == course.CourseId);
                    aViewStatus.Teacher = teacherName.First().Teacher.Name;
                    var semesterName = db.Courses.Include(c => c.Semester).Where(c => c.CourseId == course.CourseId);
                    aViewStatus.Semester = semesterName.First().Semester.Name;
                }
                else
                {

                    aViewStatus.Code = course.Code;
                    aViewStatus.Name = course.Name;
                    aViewStatus.Teacher = "Not Assigned";
                    var semesterName = db.Courses.Include(c => c.Semester).Where(c => c.CourseId == course.CourseId);
                    aViewStatus.Semester = semesterName.First().Semester.Name;
                }
                filteredViews.Add(aViewStatus);
            }
            if (filteredViews.Count <= 0) ViewBag.Message = "No courses entered.";

            return View(filteredViews);
        }

        public PartialViewResult FilterView(int? departmentId)
        {
            List<ViewCourseStatus> filteredViews = new List<ViewCourseStatus>();
            var courses = db.Courses.Where(c => c.DepartmentId !=0 );
            var selectedViews = db.CourseAssigns.Include(c => c.Teacher).Include(c => c.Course);
            if (departmentId != null)
            {
                courses = db.Courses.Where(c=>c.DepartmentId == departmentId);
                selectedViews = db.CourseAssigns.Include(c => c.Teacher).Include(c => c.Course).Where(c => (c.Course.DepartmentId == departmentId));   
            }

            
            foreach (var course in courses)
            {
                ViewCourseStatus aViewStatus = new ViewCourseStatus();
                if (db.CourseAssigns.Any(c => c.CourseId == course.CourseId))
                {
                    aViewStatus.Code = course.Code;
                    aViewStatus.Name = course.Name;
                    var teacherName  = selectedViews.Include(c => c.Teacher).Where(c => c.CourseId == course.CourseId);
                    aViewStatus.Teacher = teacherName.First().Teacher.Name;
                    var semesterName = db.Courses.Include(c => c.Semester).Where(c => c.CourseId == course.CourseId);
                    aViewStatus.Semester = semesterName.First().Semester.Name;
                }
                else
                {
                    
                    aViewStatus.Code = course.Code;
                    aViewStatus.Name = course.Name;
                    aViewStatus.Teacher = "Not Assigned";
                    var semesterName = db.Courses.Include(c => c.Semester).Where(c => c.CourseId == course.CourseId);
                    aViewStatus.Semester = semesterName.First().Semester.Name;
                }
                filteredViews.Add(aViewStatus);
            }
            if (filteredViews.Count <= 0) ViewBag.Message = "No courses entered.";
            return PartialView("~/Views/Shared/_ViewCourseStatus.cshtml", filteredViews);
        }
        /*
        [HttpPost]
        public ActionResult Index(ViewCourseStatus viewCourseStatus)
        {


            return RedirectToAction("Status", viewCourseStatus);
        }

        public ActionResult Status(ViewCourseStatus ViewCourseStatus)
        {
            var courseassignsAll = db.CourseAssigns.Include(c => c.Teacher).Include(c => c.Course);
            var courseassignsInSelectedDepartment =
                courseassignsAll.ToList().Where(c => (c.Course.DepartmentId == ViewCourseStatus.DepartmentId));
            
            List<ViewCourseStatus> viewStatuses = new List<ViewCourseStatus>();
            
            foreach (var c in courseassignsInSelectedDepartment)
            {
                ViewCourseStatus aViewStatus = new ViewCourseStatus();
                aViewStatus.Code = c.Course.Code;
                aViewStatus.Name = c.Course.Name;
                aViewStatus.Teacher = c.Teacher.Name;
                aViewStatus.Semester = c.Course.Semester.Name;
                viewStatuses.Add(aViewStatus);
            }
            return View(viewStatuses);
        }

        public PartialViewResult FilteredSection(int? departmentId)
        {
            List<ViewCourseStatus> viewStatuses = new List<ViewCourseStatus>();
            var courseassignsAll = db.CourseAssigns.Include(c => c.Teacher).Include(c => c.Course);
            if (departmentId != null)
            {
                
                var courseassignsInSelectedDepartment =
                    courseassignsAll.ToList().Where(c => (c.Course.DepartmentId == departmentId));

                

                foreach (var c in courseassignsInSelectedDepartment)
                {
                    ViewCourseStatus aViewStatus = new ViewCourseStatus();
                    aViewStatus.Code = c.Course.Code;
                    aViewStatus.Name = c.Course.Name;
                    aViewStatus.Teacher = c.Teacher.Name;
                    aViewStatus.Semester = c.Course.Semester.Name;
                    viewStatuses.Add(aViewStatus);
                }
                return PartialView("~/Views/Shared/_FilteredSection.cshtml", viewStatuses);
            }
            else
            {
                foreach (var c in courseassignsAll)
                {
                    ViewCourseStatus aViewStatus = new ViewCourseStatus();
                    aViewStatus.Code = c.Course.Code;
                    aViewStatus.Name = c.Course.Name;
                    aViewStatus.Teacher = c.Teacher.Name;
                    aViewStatus.Semester = c.Course.Semester.Name;
                    viewStatuses.Add(aViewStatus);
                }
            
                return PartialView("~/Views/Shared/_FilteredSection.cshtml", viewStatuses);
            }
        }
        

*/

    }
}
