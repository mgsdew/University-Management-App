using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityManagementApp.Models
{
    public class Course
    {
        public int CourseId { set; get; }

        
        [Remote("IsCodeExist", "Course", HttpMethod = "POST", ErrorMessage = "Course Code already exists. Please enter a different Code.")]
        [Required]
        public string Code { set; get; }
        
        
        [Remote("IsNameExist", "Course", HttpMethod = "POST", ErrorMessage = "Course Name already exists. Please enter a different Name.")]
        [Required]
        public string Name { set; get; }


        [Required]
        public double Credit { set; get; }


        [Required]
        public string Description { set; get; }


        [Required]
        public int DepartmentId { set; get; }


        [Required]
        public int SemesterId { set; get; }

       
        public virtual Department Department { set; get; }

        
        public virtual Semester Semester { set; get; }
    }
}