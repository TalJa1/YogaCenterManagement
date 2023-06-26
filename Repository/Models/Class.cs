using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class Class
    {
        public Class()
        {
            Attendances = new HashSet<Attendance>();
            ClassChangeRequestNewClasses = new HashSet<ClassChangeRequest>();
            ClassChangeRequestOldClasses = new HashSet<ClassChangeRequest>();
            Enrollments = new HashSet<Enrollment>();
            EventRequests = new HashSet<EventRequest>();
            Payments = new HashSet<Payment>();
        }

        public int ClassId { get; set; }
        public string ClassName { get; set; } = null!;
        public int? InstructorId { get; set; }
        public int? RoomId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Capacity { get; set; }
        public bool IsApproved { get; set; }
        public int SlotId { get; set; }
        public decimal MoneyNeedToPay { get; set; }

        public virtual Instructor? Instructor { get; set; }
        public virtual Room? Room { get; set; }
        public virtual Slot Slot { get; set; } = null!;
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<ClassChangeRequest> ClassChangeRequestNewClasses { get; set; }
        public virtual ICollection<ClassChangeRequest> ClassChangeRequestOldClasses { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<EventRequest> EventRequests { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
