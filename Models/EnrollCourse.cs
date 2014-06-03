using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public class EnrollCourse
    {
        public int EnrollCourseId { set; get; }
        public int? StudentId { set; get; }
        public virtual Student Student { set; get; }
        public int? CourseId { set; get; }
        public virtual Course Course { set; get; }
        public DateTime Date { set; get; }
    }
}