using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DBL.Data.Config
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FullName)
                .HasColumnType("VARCHAR")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.SpecialtyId)
                .IsRequired();

            builder.Property(x => x.PhoneNumber)
                .HasColumnType("VARCHAR")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.YearsOfExperience)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnType("VARCHAR")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Gender)
                .HasColumnType("VARCHAR")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.HasOne(x => x.Specialty)
                .WithMany(x => x.Doctors)
                .HasForeignKey(x => x.SpecialtyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Doctors");

            builder.HasData(LoadDoctor());
        }

        private static List<Doctor> LoadDoctor()
        {
            return new List<Doctor>
            {
                new Doctor
                {
                    Id = 1,
                    FullName = "Ahmad Ali",
                    SpecialtyId = 1,
                    PhoneNumber = "0790000001",
                    YearsOfExperience = 10,
                    Email = "ahmad.ali@gmail.com",
                    Gender = "Male",
                    IsActive = true
                },

                new Doctor
                {
                    Id = 2,
                    FullName = "Khaled Omar",
                    SpecialtyId = 2,
                    PhoneNumber = "0790000002",
                    YearsOfExperience = 8,
                    Email = "khaled.omar@gmail.com",
                    Gender = "Male",
                    IsActive = true
                },

                new Doctor
                {
                    Id = 3,
                    FullName = "Mohammad Ahmad",
                    SpecialtyId = 3,
                    PhoneNumber = "0790000003",
                    YearsOfExperience = 6,
                    Email = "mohammad.ahmad@gmail.com",
                    Gender = "Male",
                    IsActive = true
                },

                new Doctor
                {
                    Id = 4,
                    FullName = "Omar Hassan",
                    SpecialtyId = 4,
                    PhoneNumber = "0790000004",
                    YearsOfExperience = 12,
                    Email = "omar.hassan@gmail.com",
                    Gender = "Male",
                    IsActive = true
                },

                new Doctor
                {
                    Id = 5,
                    FullName = "Yousef Mahmoud",
                    SpecialtyId = 5,
                    PhoneNumber = "0790000005",
                    YearsOfExperience = 15,
                    Email = "yousef.mahmoud@gmail.com",
                    Gender = "Male",
                    IsActive = false
                },

                new Doctor
                {
                    Id = 6,
                    FullName = "Sara Khalil",
                    SpecialtyId = 6,
                    PhoneNumber = "0790000006",
                    YearsOfExperience = 7,
                    Email = "sara.khalil@gmail.com",
                    Gender = "Female",
                    IsActive = true
                },

                new Doctor
                {
                    Id = 7,
                    FullName = "Lina Ahmad",
                    SpecialtyId = 7,
                    PhoneNumber = "0790000007",
                    YearsOfExperience = 5,
                    Email = "lina.ahmad@gmail.com",
                    Gender = "Female",
                    IsActive = true
                },

                new Doctor
                {
                    Id = 8,
                    FullName = "Rami Samir",
                    SpecialtyId = 8,
                    PhoneNumber = "0790000008",
                    YearsOfExperience = 9,
                    Email = "rami.samir@gmail.com",
                    Gender = "Male",
                    IsActive = false
                },

                new Doctor
                {
                    Id = 9,
                    FullName = "Tareq Nasser",
                    SpecialtyId = 9,
                    PhoneNumber = "0790000009",
                    YearsOfExperience = 11,
                    Email = "tareq.nasser@gmail.com",
                    Gender = "Male",
                    IsActive = true
                },

                new Doctor
                {
                    Id = 10,
                    FullName = "Hassan Saleh",
                    SpecialtyId = 10,
                    PhoneNumber = "0790000010",
                    YearsOfExperience = 13,
                    Email = "hassan.saleh@gmail.com",
                    Gender = "Male",
                    IsActive = true
                }
            };
        }
    }
}