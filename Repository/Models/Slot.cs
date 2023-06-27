using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class Slot
    {
        public Slot()
        {
            Classes = new HashSet<Class>();
        }

        public int SlotId { get; set; }
        public string SlotName { get; set; } = null!;

        public virtual ICollection<Class> Classes { get; set; }
    }
}
