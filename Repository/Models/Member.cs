using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class Member
    {
        public Member()
        {
            Attendances = new HashSet<Attendance>();
            ClassChangeRequests = new HashSet<ClassChangeRequest>();
            Enrollments = new HashSet<Enrollment>();
            EquipmentRentals = new HashSet<EquipmentRental>();
            Instructors = new HashSet<Instructor>();
            Payments = new HashSet<Payment>();
        }

        public int MemberId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;

        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<ClassChangeRequest> ClassChangeRequests { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<EquipmentRental> EquipmentRentals { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
