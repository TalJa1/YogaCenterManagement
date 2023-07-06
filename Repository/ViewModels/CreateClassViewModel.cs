using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ViewModels
{
    public class CreateClassViewModel
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; } = null!;
        public int? InstructorId { get; set; }
        public int? RoomId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Capacity { get; set; }
        public bool IsApproved { get; set; }
        public int SlotId { get; set; }
        public decimal MoneyNeedToPay { get; set; }

        public virtual Instructor? Instructor { get; set; }
        public virtual Room? Room { get; set; }
        public Slot Slot { get; set; }
    }
}
