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
    public class ClincConfiguration:IEntityTypeConfiguration<Clinc>
    {
        public void Configure(EntityTypeBuilder<Clinc> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ClincCode)
              .IsRequired();
            builder.Property(x => x.ClincName)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(x => x.Specialization)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(x => x.DoctorNo)
              .IsRequired();
            builder.Property(x => x.NumberOfPatients)
              .IsRequired();
            builder.Property(x => x.IsActive)
                .IsRequired();
            builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);
            builder.Property(x => x.Code)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(50);
            builder.HasOne(x=>x.Doctor)
                .WithMany(x=>x.Clincs)
                .HasForeignKey(x=>x.DoctorNo)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.StartTime)
              .HasColumnType("time")
              .IsRequired();
            builder.Property(x => x.EndTime)
               .HasColumnType("time")
               .IsRequired();
            builder.ToTable("Clinc");
        }

    }
}
