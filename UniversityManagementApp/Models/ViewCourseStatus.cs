using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public class ViewCourseStatus
    {
        public int  DepartmentId { set; get; }
        public string Code { set; get; }
        public string Name { set; get; }
        public string Teacher { set; get; }
        public string Semester { set; get; }
    }
}