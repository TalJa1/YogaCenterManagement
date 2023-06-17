using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

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
        public virtual DbSet<Enrollment> Enrollments { get; set; } = null!;
        public virtual DbSet<Equipment> Equipment { get; set; } = null!;
        public virtual DbSet<EquipmentRental> EquipmentRentals { get; set; } = null!;
        public virtual DbSet<EventRequest> EventRequests { get; set; } = null!;
        public virtual DbSet<Instructor> Instructors { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<SalaryChangeRequest> SalaryChangeRequests { get; set; } = null!;

        public string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();
            var strConn = config["ConnectionStrings:YogaManagement"];
            return strConn;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
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
                    .HasConstraintName("FK__Attendanc__class__60A75C0F");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK__Attendanc__membe__619B8048");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("class_name");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time");

                entity.Property(e => e.InstructorId).HasColumnName("instructor_id");

                entity.Property(e => e.IsApproved).HasColumnName("is_approved");

                entity.Property(e => e.MoneyNeedToPay)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("money_need_to_pay");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.InstructorId)
                    .HasConstraintName("FK__Classes__instruc__5535A963");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__Classes__room_id__5629CD9C");
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
                    .HasConstraintName("FK__Enrollmen__class__59FA5E80");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK__Enrollmen__membe__59063A47");
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
                    .HasName("PK__Equipmen__67DB611B04067AD9");

                entity.Property(e => e.RentalId).HasColumnName("rental_id");

                entity.Property(e => e.EquipmentId).HasColumnName("equipment_id");

                entity.Property(e => e.IsApproved).HasColumnName("is_approved");

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
                    .HasConstraintName("FK__Equipment__equip__68487DD7");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.EquipmentRentals)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK__Equipment__membe__6754599E");
            });

            modelBuilder.Entity<EventRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK__EventReq__18D3B90FF41B9E52");

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.EventDate)
                    .HasColumnType("datetime")
                    .HasColumnName("event_date");

                entity.Property(e => e.EventName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("event_name");

                entity.Property(e => e.InstructorId).HasColumnName("instructor_id");

                entity.Property(e => e.IsApproved).HasColumnName("is_approved");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.EventRequests)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK__EventRequ__class__6D0D32F4");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.EventRequests)
                    .HasForeignKey(d => d.InstructorId)
                    .HasConstraintName("FK__EventRequ__instr__6C190EBB");
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
                    .HasConstraintName("FK__Instructo__membe__4CA06362");
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
                    .HasConstraintName("FK__Payments__class___5DCAEF64");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK__Payments__member__5CD6CB2B");
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
                    .HasName("PK__SalaryCh__18D3B90F86B54ECD");

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
                    .HasConstraintName("FK__SalaryCha__instr__4F7CD00D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
