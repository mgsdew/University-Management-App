using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.Controllers
{
    public class TeacherCourseSelectorController : Controller
    {
        //
        // GET: /TeacherCourseSelector/
        UniversityDbContext Db = new UniversityDbContext();

        public ActionResult Index()
        {
            return View();
        }

    }
}
