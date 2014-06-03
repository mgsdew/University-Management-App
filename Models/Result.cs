using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public class Result
    {
        public int ResultId { set; get; }
        
        public int StudentId { set; get; }
        
        [DisplayName("Student Registration No.")]
        public virtual Student Student { set; get; }
        
        public int? CourseId { set; get; }
        
        [DisplayName("Select Course")]
        public virtual Course Course { set; get; }
       
        public int? ResultGradeId { set; get; }
        
        [DisplayName("Select Grade Letter")]
        public ResultGrade ResultGrade { set; get; }

    }
}