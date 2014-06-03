using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public class UniversityDbContext: DbContext
    {
        public DbSet<Department> Departments { set; get; }
        public DbSet<Course> Courses { set; get; }
        public DbSet<Semester> Semesters { set; get; }
        public DbSet<Designation> Designations { set; get; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<CourseAssign> CourseAssigns { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<EnrollCourse> EnrollCourses { get; set; }

        public DbSet<ResultGrade> ResultGrades { get; set; }

        public DbSet<Result> Results { get; set; }

        public DbSet<ClassRoom> ClassRooms { get; set; }

        public DbSet<WeekDay> WeekDays { get; set; }

        public DbSet<AllocateClassRoom> AllocateClassRooms { get; set; }



    }
}