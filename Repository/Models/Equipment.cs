using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class Equipment
    {
        public Equipment()
        {
            EquipmentRentals = new HashSet<EquipmentRental>();
        }

        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; } = null!;
        public int Quantity { get; set; }

        public virtual ICollection<EquipmentRental> EquipmentRentals { get; set; }
    }
}
