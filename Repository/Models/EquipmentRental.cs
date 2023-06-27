using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class EquipmentRental
    {
        public int RentalId { get; set; }
        public int? MemberId { get; set; }
        public int? EquipmentId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturn { get; set; }
        public bool? Isapprove { get; set; }

        public virtual Equipment? Equipment { get; set; }
        public virtual Member? Member { get; set; }
    }
}
