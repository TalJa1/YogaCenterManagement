using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class ClassChangeRequest
    {
        public int RequestId { get; set; }
        public int? MemberId { get; set; }
        public int? ClassId { get; set; }
        public DateTime RequestDate { get; set; }
        public bool? IsApproved { get; set; }

        public virtual Class? Class { get; set; }
        public virtual Member? Member { get; set; }
    }
}
