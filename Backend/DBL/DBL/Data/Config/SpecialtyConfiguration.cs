using DBL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL.Data.Config
{
    public class SpecialtyConfiguration:IEntityTypeConfiguration<Specialty>
    {
        public void Configure(EntityTypeBuilder<Specialty> builder)

        {

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(x => x.Code)
                .HasColumnType("VARCHAR")
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(x => x.IsActive)
                .IsRequired();
            builder.Property(x => x.IsDelete)
              .HasDefaultValue(false);
            builder.ToTable("Specialties");
            builder.HasData(
    new Specialty
    {
        Id = 1,
        Name = "Cardiology",
        Code = "CARD",
        IsActive = true
    },

    new Specialty
    {
        Id = 2,
        Name = "Dermatology",
        Code = "DERM",
        IsActive = true
    },

    new Specialty
    {
        Id = 3,
        Name = "Pediatrics",
        Code = "PED",
        IsActive = true
    },

    new Specialty
    {
        Id = 4,
        Name = "Orthopedics",
        Code = "ORTH",
        IsActive = true
    },

    new Specialty
    {
        Id = 5,
        Name = "Neurology",
        Code = "NEUR",
        IsActive = true
    },

    new Specialty
    {
        Id = 6,
        Name = "Gynecology",
        Code = "GYN",
        IsActive = true
    },

    new Specialty
    {
        Id = 7,
        Name = "Ophthalmology",
        Code = "OPHT",
        IsActive = true
    },

    new Specialty
    {
        Id = 8,
        Name = "ENT",
        Code = "ENT",
        IsActive = true
    },

    new Specialty
    {
        Id = 9,
        Name = "General Surgery",
        Code = "SURG",
        IsActive = true
    },

    new Specialty
    {
        Id = 10,
        Name = "Internal Medicine",
        Code = "INT",
        IsActive = true
    }
);
        }
    }
}
