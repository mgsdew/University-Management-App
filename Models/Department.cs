using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityManagementApp.Models
{
    public class Department
    {
        
        public int DepartmentId { set; get; }
        
        [Remote("doesDepartmentNameExist", "Department", HttpMethod = "POST", ErrorMessage = "Department name already exists. Please enter a different name.")]
        [Required] public string Name { set; get; }

        [Remote("doesDepartmentCodeExist", "Department", HttpMethod = "POST", ErrorMessage = "Department Code already exists. Please enter a different Code.")]
        [Required] public string Code { set; get; }
    }
}