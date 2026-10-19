using DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL.Data.Config
{
    public class AppointmentConfiguration:IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.AppointmentCode)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(50)
               .IsRequired();
            builder.Property(x => x.StartTime)
               .HasColumnType("time")
               .IsRequired();
            builder.Property(x => x.EndTime)
               .HasColumnType("time")
               .IsRequired();
            builder.Property(x => x.AppointmentDate)
              .HasColumnType("date");
            builder.Property(x => x.Notes)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(50);
            builder.Property(x => x.CreatedAt)
              .HasColumnType("date")
              .IsRequired();
            builder.Property(x => x.IsDeleted)
               .HasDefaultValue(false);
            builder.Property(x => x.Status)
               .HasConversion<string>()
               .IsRequired();
            builder.HasOne(x => x.Doctor)
                .WithMany(x => x.Appointments)
                .HasForeignKey(x => x.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Patient)
                .WithMany(x => x.Appointments)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Clinc)
                .WithMany(x => x.Appointments)
                .HasForeignKey(x => x.ClincId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.MadicalService)
                .WithMany(x => x.Appointments)
                .HasForeignKey(x => x.MedicalServiceId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.ToTable("Appointment");

        }
    }
}
