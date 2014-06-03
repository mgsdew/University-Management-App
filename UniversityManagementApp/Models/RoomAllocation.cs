using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public class RoomAllocation
    {
        public int RoomAllcationId { set; get; }
        public int? DepartmentId { set; get; }
        public int? CourseId { set; get; }
        public int? RoomNo { set; get; }
        public DateTime StarTime { set; get; }
        public DateTime EndTime { set; get; }

    }
}