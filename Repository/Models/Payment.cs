using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public int? MemberId { get; set; }
        public int? ClassId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public virtual Class? Class { get; set; }
        public virtual Member? Member { get; set; }
    }
}
