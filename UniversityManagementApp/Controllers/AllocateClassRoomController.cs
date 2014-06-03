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
    public class AllocateClassRoomController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        //
        // GET: /AllocateClassRoom/

        public ActionResult Index()
        {
            var allocateclassrooms = db.AllocateClassRooms.Include(a => a.Department).Include(a => a.Course).Include(a => a.ClassRoom).Include(a => a.WeekDay);
            return View(allocateclassrooms.ToList());
        }

        public ActionResult ViewClassScheduleAndAllocation()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments,"DepartmentId","Code");
            var allocateclassrooms = db.AllocateClassRooms.Include(a => a.Department).Include(a => a.Course).Include(a => a.ClassRoom).Include(a => a.WeekDay);
            return View(allocateclassrooms.ToList());
        }

        //
        // GET: /AllocateClassRoom/Details/5

        public ActionResult Details(int id = 0)
        {
            AllocateClassRoom allocateclassroom = db.AllocateClassRooms.Find(id);
            if (allocateclassroom == null)
            {
                return HttpNotFound();
            }
            return View(allocateclassroom);
        }

        //
        // GET: /AllocateClassRoom/Create

        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name");
            ViewBag.CourseId = new SelectList(db.Courses.Where(c => c.CourseId == 0), "CourseId", "Code");
            ViewBag.ClassRoomId = new SelectList(db.ClassRooms, "ClassRoomId", "RoomNo");
            ViewBag.WeekDayId = new SelectList(db.WeekDays, "WeekDayId", "Day");
            return View();
        }

        //
        // POST: /AllocateClassRoom/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AllocateClassRoom allocateclassroom)
        {
            if (ModelState.IsValid)
            {
                db.AllocateClassRooms.Add(allocateclassroom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

           

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", allocateclassroom.DepartmentId);
            ViewBag.CourseId = new SelectList(db.Courses.Where(c => c.CourseId == 0), "CourseId", "Code", allocateclassroom.CourseId);
            ViewBag.ClassRoomId = new SelectList(db.ClassRooms, "ClassRoomId", "RoomNo", allocateclassroom.ClassRoomId);
            ViewBag.WeekDayId = new SelectList(db.WeekDays, "WeekDayId", "Day", allocateclassroom.WeekDayId);
            return View(allocateclassroom);
        }

        public List<int> Bookedtime(int start, int end)
        {
            List<int> bookedTimes = new List<int>();
            for (int i = start; i <= end; i++)
            {
                bookedTimes.Add(i);
            }
            return bookedTimes;
        }

       //
        // GET: /AllocateClassRoom/Edit/5

        public ActionResult Edit(int id = 0)
        {
            AllocateClassRoom allocateclassroom = db.AllocateClassRooms.Find(id);
            if (allocateclassroom == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", allocateclassroom.DepartmentId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", allocateclassroom.CourseId);
            ViewBag.ClassRoomId = new SelectList(db.ClassRooms, "ClassRoomId", "RoomNo", allocateclassroom.ClassRoomId);
            ViewBag.WeekDayId = new SelectList(db.WeekDays, "WeekDayId", "Day", allocateclassroom.WeekDayId);
            return View(allocateclassroom);
        }

        //
        // POST: /AllocateClassRoom/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AllocateClassRoom allocateclassroom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(allocateclassroom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", allocateclassroom.DepartmentId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", allocateclassroom.CourseId);
            ViewBag.ClassRoomId = new SelectList(db.ClassRooms, "ClassRoomId", "RoomNo", allocateclassroom.ClassRoomId);
            ViewBag.WeekDayId = new SelectList(db.WeekDays, "WeekDayId", "Day", allocateclassroom.WeekDayId);
            return View(allocateclassroom);
        }

        //
        // GET: /AllocateClassRoom/Delete/5

        public ActionResult Delete(int id = 0)
        {
            AllocateClassRoom allocateclassroom = db.AllocateClassRooms.Find(id);
            if (allocateclassroom == null)
            {
                return HttpNotFound();
            }
            return View(allocateclassroom);
        }

        //
        // POST: /AllocateClassRoom/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AllocateClassRoom allocateclassroom = db.AllocateClassRooms.Find(id);
            db.AllocateClassRooms.Remove(allocateclassroom);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        public PartialViewResult FilterView(int? departmentId)
        {
            if (departmentId != null)
            {
                var dptCors = db.Courses.Where(d => d.DepartmentId == departmentId);

                List<ViewClassSchedule> clssch = new List<ViewClassSchedule>();
                foreach (Course course in dptCors)
                {
                    ViewClassSchedule aViewClassSchedule = new ViewClassSchedule();
                    aViewClassSchedule.CourseName = course.Name;
                    aViewClassSchedule.CourseCode = course.Code;
                    aViewClassSchedule.ScheduleInfo = "";
                    var al =
                        db.AllocateClassRooms.Include(a => a.Department)
                            .Include(a => a.Course)
                            .Include(a => a.ClassRoom)
                            .Include(a => a.WeekDay)
                            .Where(a => a.CourseId == course.CourseId);
                    foreach (var all in al)
                    {
                        aViewClassSchedule.ScheduleInfo += "Room No: " + all.ClassRoom.RoomNo + " " + all.WeekDay.Day +
                                                           " " +
                                                           all.StarTimeHour + ":" + all.StarTimeMin + "-" +
                                                           all.EndTimeHour +
                                                           ":" + all.EndTimeMin + ";";
                        
                    }
                    clssch.Add(aViewClassSchedule);

                }
                return PartialView("~/Views/Shared/_ViewClassScheduleAndAllocation.cshtml", clssch);
            }
            else
            {
                List<ViewClassSchedule> clssch = new List<ViewClassSchedule>();
                ViewBag.message = "Select a department";
                return PartialView("~/Views/Shared/_ViewClassScheduleAndAllocation.cshtml", clssch);
            }
        }

    }
}