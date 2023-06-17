using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class Class
    {
        public Class()
        {
            Attendances = new HashSet<Attendance>();
            Enrollments = new HashSet<Enrollment>();
            EventRequests = new HashSet<EventRequest>();
            Payments = new HashSet<Payment>();
        }

        public int ClassId { get; set; }
        public string ClassName { get; set; } = null!;
        public int? InstructorId { get; set; }
        public int? RoomId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Capacity { get; set; }
        public bool IsApproved { get; set; }
        public decimal MoneyNeedToPay { get; set; }

        public virtual Instructor? Instructor { get; set; }
        public virtual Room? Room { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<EventRequest> EventRequests { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
