using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public class CourseAssignToTeacher
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public virtual Teacher SelecTeacher { get; set; }
        public virtual Course SelectCourse { get; set; }
    }
}