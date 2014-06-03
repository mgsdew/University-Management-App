using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public class AllocateClassRoom
    {
        public int AllocateClassRoomId { set; get; }
        public int? DepartmentId { set; get; }
        [DisplayName("Department")]
        public virtual Department Department { set; get; }
        public int? CourseId { set; get; }
        [DisplayName("Course")]
        public virtual Course Course { set; get; }
        public int? ClassRoomId { set; get; }
        public virtual ClassRoom ClassRoom { set; get; }
        public int? WeekDayId { set; get; }
        public virtual WeekDay WeekDay { set; get; }
        [DisplayName("Start Time")]
        public int StarTimeHour { set; get; }
        public int StarTimeMin { set; get; }
        [DisplayName("End Time")]
        public int EndTimeHour { set; get; }
        public int EndTimeMin { set; get; }

       
    }
}