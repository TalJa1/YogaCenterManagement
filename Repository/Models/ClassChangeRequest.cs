using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class ClassChangeRequest
    {
        public int RequestId { get; set; }
        public int? MemberId { get; set; }
        public int? OldClassId { get; set; }
        public int? NewClassId { get; set; }
        public DateTime RequestDate { get; set; }
        public bool? IsApproved { get; set; }

        public virtual Member? Member { get; set; }
        public virtual Class? NewClass { get; set; }
        public virtual Class? OldClass { get; set; }
    }
}
