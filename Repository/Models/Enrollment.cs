using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int? MemberId { get; set; }
        public int? ClassId { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public virtual Class? Class { get; set; }
        public virtual Member? Member { get; set; }
    }
}
