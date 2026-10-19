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
    public class DoctorScheduleConfiguration
    {
        public void Configure(EntityTypeBuilder<DoctorSchedule> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DayOfWeek)
            .IsRequired();
            builder.Property(x => x.StarTime)
            .HasColumnType("time")
            .IsRequired();
            builder.Property(x => x.EndTime)
            .HasColumnType("time")
            .IsRequired();
            builder.Property(x => x.SlotDurationMinutes)
            .IsRequired();
            builder.Property(x => x.IsDeleted)
                  .HasDefaultValue(false);
            builder.HasOne(x => x.Doctor)
                   .WithMany(x => x.DoctorSchedules)
                   .HasForeignKey(x => x.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.ToTable("DoctorSchedules");
        }
    }
}
