using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityManagementApp.Models
{
    public class Teacher
    {
        public int TeacherId { set; get; }
        
        
        [Required]
        public string Name { set; get; }


        [Required]
        public string Address { set; get; }


        [Required]
        [Remote("IsEmailExist", "Teacher", HttpMethod = "POST", ErrorMessage = "Email already exists. Please enter a different Email.")]
        public string Email { set; get; }


        [Required]
        [DisplayName("Contact Number")]
        public string ContactNo { set; get; }


        [Required]
        public int DesignationId { set; get; }


        [Required]
        public int DepartmentId { set; get; }


        [Required]
        [DisplayName("Total Credit to be taken")]
        public double TotalCredit { set; get; }



        public virtual Department Department { set; get; }
        public virtual Designation Designation { set; get; }
    }
}