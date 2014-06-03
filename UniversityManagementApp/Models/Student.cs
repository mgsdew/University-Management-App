using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public class Student
    {
        public int StudentId { set; get; }
        [Required]
        public string Name { set; get; }
        
        public string RegNo { set; get; }
        [Required]
        public string Email { set; get; }
        [Required]
        public string ContactcNo { set; get; }
        
        public DateTime RegistrationDate { set; get; }
        [Required]
        public string Address { set; get; }
        
        [DisplayName("Department")]
        public int DepartmentId { set; get; }
        
        [DisplayName("Department")]
        public virtual Department Department { set; get; }
    }
}