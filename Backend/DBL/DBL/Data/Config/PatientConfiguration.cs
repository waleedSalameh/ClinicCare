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
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FullName)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(50)
               .IsRequired();
            builder.Property(x => x.Code)
               .IsRequired();
            builder.Property(x => x.NationalId)
               .HasColumnType("VARCHAR")
               .HasMaxLength(50)
               .IsRequired();
            builder.Property(x => x.PhoneNumber)
                .HasColumnType("VARCHAR")
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(x => x.DateOfBirth)
              .HasColumnType("date");
            builder.Property(x => x.Gender)
                .IsRequired();
            builder.Property(x => x.Address)
                .HasColumnType("VARCHAR")
                .HasMaxLength(50);
            builder.Property(x => x.IsDeleted)
               .HasDefaultValue(false);
            builder.HasData(
               new Patient
               {
                   Id = 1,
                   Code = 1001,
                   FullName = "Ahmad Ali",
                   NationalId = "99001122",
                   PhoneNumber = "0799001122",
                   DateOfBirth = new DateTime(1990, 5, 15),
                   Gender = 1,
                   Address = "Amman",
                   IsDeleted = false
               },
               new Patient
               {
                   Id = 2,
                   Code = 1002,
                   FullName = "Sara Mohammad",
                   NationalId = "99112233",
                   PhoneNumber = "0799112233",
                   DateOfBirth = new DateTime(1995, 8, 20),
                   Gender = 2,
                   Address = "Zarqa",
                   IsDeleted = false
               },
               new Patient
               {
                   Id = 3,
                   Code = 1003,
                   FullName = "Omar Khaled",
                   NationalId ="99223344",
                   PhoneNumber = "0789223344",
                   DateOfBirth = new DateTime(1987, 2, 10),
                   Gender = 1,
                   Address = "Irbid",
                   IsDeleted = false
               },
               new Patient
               {
                   Id = 4,
                   Code = 1004,
                   FullName = "Lina Ahmad",
                   NationalId = "99334455",
                   PhoneNumber = "0779334455",
                   DateOfBirth = new DateTime(2000, 11, 5),
                   Gender = 2,
                   Address = "Amman",
                   IsDeleted = false
               },
               new Patient
               {
                   Id = 5,
                   Code = 1005,
                   FullName = "Yousef Mahmoud",
                   NationalId = "99445566",
                   PhoneNumber = "0798445566",
                   DateOfBirth = new DateTime(1982, 7, 25),
                   Gender = 1,
                   Address = "Salt",
                   IsDeleted = false
               }
           );


        }
    }
}
