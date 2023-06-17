using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class Attendance
    {
        public int AttendanceId { get; set; }
        public int? ClassId { get; set; }
        public int? MemberId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public bool IsPresent { get; set; }

        public virtual Class? Class { get; set; }
        public virtual Member? Member { get; set; }
    }
}
