using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public class TeacherCourseSelector
    {
        public int DepartmentId { set; get; }
        public virtual Department Department { set; get; }
        public int TeacherId { set; get; }
        public virtual Teacher Teacher { set; get; }
    }
}