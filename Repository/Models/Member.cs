using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repository.Models
{
    public partial class Member
    {
        public Member()
        {
            Attendances = new HashSet<Attendance>();
            Enrollments = new HashSet<Enrollment>();
            EquipmentRentals = new HashSet<EquipmentRental>();
            Instructors = new HashSet<Instructor>();
            Payments = new HashSet<Payment>();
        }

        public int MemberId { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }

        [Required(ErrorMessage = "FullName is required.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<EquipmentRental> EquipmentRentals { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
