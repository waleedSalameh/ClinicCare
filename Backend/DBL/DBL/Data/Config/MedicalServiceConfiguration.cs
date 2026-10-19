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
    public class MedicalServiceConfiguration : IEntityTypeConfiguration<MadicalService>
    {
        public void Configure(EntityTypeBuilder<MadicalService> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ServiceName)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(x => x.ServiceCode)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(50)
               .IsRequired();
            builder.Property(x => x.Desription)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(200)
               .IsRequired();
            builder.Property(x => x.IsActive)
                .IsRequired();
            builder.Property(x => x.Price)
                .IsRequired();
            builder.Property(x => x.IsDeleted)
             .HasDefaultValue(false);
            builder.Property(x => x.DurationInMinutes)
              .IsRequired();
            builder.Property(x => x.CostPrice)
              .IsRequired();
            builder.Property(x => x.SellingPrice)
              .IsRequired();
            builder.Property(x => x.Clincid);
            builder.HasOne(x => x.Clinc)
                .WithMany(x => x.MadicalServices)
                .HasForeignKey(x => x.Clincid)
                .OnDelete(DeleteBehavior.Restrict);
            builder.ToTable("MadicalService");
        }
    }
}
