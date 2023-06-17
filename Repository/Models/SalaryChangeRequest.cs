using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class SalaryChangeRequest
    {
        public int RequestId { get; set; }
        public int? InstructorId { get; set; }
        public decimal NewSalary { get; set; }
        public DateTime RequestDate { get; set; }
        public bool? IsApproved { get; set; }

        public virtual Instructor? Instructor { get; set; }
    }
}
