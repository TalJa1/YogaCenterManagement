using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class EventRequest
    {
        public int RequestId { get; set; }
        public int? InstructorId { get; set; }
        public int? ClassId { get; set; }
        public string EventName { get; set; } = null!;
        public bool IsApproved { get; set; }
        public string EventDescription { get; set; } = null!;

        public virtual Class? Class { get; set; }
        public virtual Instructor? Instructor { get; set; }
    }
}
