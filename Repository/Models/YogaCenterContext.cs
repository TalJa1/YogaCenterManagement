using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Repository.Models
{
    public partial class YogaCenterContext : DbContext
    {
        public YogaCenterContext()
        {
        }

        public YogaCenterContext(DbContextOptions<YogaCenterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attendance> Attendances { get; set; } = null!;
        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<ClassChangeRequest> ClassChangeRequests { get; set; } = null!;
        public virtual DbSet<Enrollment> Enrollments { get; set; } = null!;
        public virtual DbSet<Equipment> Equipment { get; set; } = null!;
        public virtual DbSet<EquipmentRental> EquipmentRentals { get; set; } = null!;
        public virtual DbSet<EventRequest> EventRequests { get; set; } = null!;
        public virtual DbSet<Instructor> Instructors { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<SalaryChangeRequest> SalaryChangeRequests { get; set; } = null!;
        public virtual DbSet<Slot> Slots { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
<<<<<<< Updated upstream
                optionsBuilder.UseSqlServer("Server=(local);Initial Catalog=YogaCenter;Uid=sa;Pwd=123456789;TrustServerCertificate=True");
=======
                optionsBuilder.UseSqlServer("Server=(local);Initial Catalog=YogaCenter;Uid=sa;Pwd=123;TrustServerCertificate=True");
>>>>>>> Stashed changes
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.ToTable("Attendance");

                entity.Property(e => e.AttendanceId).HasColumnName("attendance_id");

                entity.Property(e => e.AttendanceDate)
                    .HasColumnType("date")
                    .HasColumnName("attendance_date");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.IsPresent).HasColumnName("is_present");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK__Attendanc__class__6383C8BA");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK__Attendanc__membe__6477ECF3");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.BeginDate)
                    .HasColumnType("date")
                    .HasColumnName("begin_date");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("class_name");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.EndTime).HasColumnName("end_time");

                entity.Property(e => e.InstructorId).HasColumnName("instructor_id");

                entity.Property(e => e.IsApproved).HasColumnName("is_approved");

                entity.Property(e => e.MoneyNeedToPay)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("money_need_to_pay");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.SlotId).HasColumnName("slotId");

                entity.Property(e => e.StartTime).HasColumnName("start_time");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.InstructorId)
                    .HasConstraintName("FK__Classes__instruc__571DF1D5");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__Classes__room_id__5812160E");

                entity.HasOne(d => d.Slot)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.SlotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Classes__slotId__59063A47");
            });

            modelBuilder.Entity<ClassChangeRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK__ClassCha__18D3B90F19B007A6");

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.Property(e => e.IsApproved).HasColumnName("is_approved");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.NewClassId).HasColumnName("new_class_id");

                entity.Property(e => e.OldClassId).HasColumnName("old_class_id");

                entity.Property(e => e.RequestDate)
                    .HasColumnType("date")
                    .HasColumnName("request_date");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.ClassChangeRequests)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK__ClassChan__membe__72C60C4A");

                entity.HasOne(d => d.NewClass)
                    .WithMany(p => p.ClassChangeRequestNewClasses)
                    .HasForeignKey(d => d.NewClassId)
                    .HasConstraintName("FK__ClassChan__new_c__74AE54BC");

                entity.HasOne(d => d.OldClass)
                    .WithMany(p => p.ClassChangeRequestOldClasses)
                    .HasForeignKey(d => d.OldClassId)
                    .HasConstraintName("FK__ClassChan__old_c__73BA3083");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.Property(e => e.EnrollmentId).HasColumnName("enrollment_id");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.EnrollmentDate)
                    .HasColumnType("date")
                    .HasColumnName("enrollment_date");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK__Enrollmen__class__5CD6CB2B");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK__Enrollmen__membe__5BE2A6F2");
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.Property(e => e.EquipmentId).HasColumnName("equipment_id");

                entity.Property(e => e.EquipmentName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("equipment_name");

                entity.Property(e => e.Quantity).HasColumnName("quantity");
            });

            modelBuilder.Entity<EquipmentRental>(entity =>
            {
                entity.HasKey(e => e.RentalId)
                    .HasName("PK__Equipmen__67DB611B68BF6DE9");

                entity.Property(e => e.RentalId).HasColumnName("rental_id");

                entity.Property(e => e.EquipmentId).HasColumnName("equipment_id");

                entity.Property(e => e.IsReturn).HasColumnName("isReturn");

                entity.Property(e => e.Isapprove).HasColumnName("isapprove");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.RentalDate)
                    .HasColumnType("date")
                    .HasColumnName("rental_date");

                entity.Property(e => e.ReturnDate)
                    .HasColumnType("date")
                    .HasColumnName("return_date");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.EquipmentRentals)
                    .HasForeignKey(d => d.EquipmentId)
                    .HasConstraintName("FK__Equipment__equip__6B24EA82");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.EquipmentRentals)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK__Equipment__membe__6A30C649");
            });

            modelBuilder.Entity<EventRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK__EventReq__18D3B90FB65E8104");

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.EventDescription)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("event_description");

                entity.Property(e => e.EventName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("event_name");

                entity.Property(e => e.InstructorId).HasColumnName("instructor_id");

                entity.Property(e => e.IsApproved).HasColumnName("is_approved");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.EventRequests)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK__EventRequ__class__6FE99F9F");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.EventRequests)
                    .HasForeignKey(d => d.InstructorId)
                    .HasConstraintName("FK__EventRequ__instr__6EF57B66");
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.Property(e => e.InstructorId).HasColumnName("instructor_id");

                entity.Property(e => e.IsSalaryChangeRequested).HasColumnName("is_salary_change_requested");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.Salary)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("salary");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Instructors)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK__Instructo__membe__4E88ABD4");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("full_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Role)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("role");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("date")
                    .HasColumnName("payment_date");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK__Payments__class___60A75C0F");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK__Payments__member__5FB337D6");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.RoomName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("room_name");
            });

            modelBuilder.Entity<SalaryChangeRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK__SalaryCh__18D3B90F485CA05D");

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.Property(e => e.InstructorId).HasColumnName("instructor_id");

                entity.Property(e => e.IsApproved).HasColumnName("is_approved");

                entity.Property(e => e.NewSalary)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("new_salary");

                entity.Property(e => e.RequestDate)
                    .HasColumnType("date")
                    .HasColumnName("request_date");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.SalaryChangeRequests)
                    .HasForeignKey(d => d.InstructorId)
                    .HasConstraintName("FK__SalaryCha__instr__5165187F");
            });

            modelBuilder.Entity<Slot>(entity =>
            {
                entity.Property(e => e.SlotId)
                    .ValueGeneratedNever()
                    .HasColumnName("slotId");

                entity.Property(e => e.SlotName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("slotName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
