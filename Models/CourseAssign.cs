using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public class CourseAssign
    {
        public int CourseAssignId { set; get; }
        public int? TeacherId { set; get; }

        public int? CourseId { set; get; }

        public virtual Teacher Teacher { get; set; }

        public virtual Course Course { get; set; }
    }
}