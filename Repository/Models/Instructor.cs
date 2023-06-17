using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class Instructor
    {
        public Instructor()
        {
            Classes = new HashSet<Class>();
            EventRequests = new HashSet<EventRequest>();
            SalaryChangeRequests = new HashSet<SalaryChangeRequest>();
        }

        public int InstructorId { get; set; }
        public int? MemberId { get; set; }
        public decimal Salary { get; set; }
        public bool IsSalaryChangeRequested { get; set; }

        public virtual Member? Member { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<EventRequest> EventRequests { get; set; }
        public virtual ICollection<SalaryChangeRequest> SalaryChangeRequests { get; set; }
    }
}
