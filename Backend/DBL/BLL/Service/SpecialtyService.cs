using BLL.Interfaces;
using BLL.Interfaces.BLL.Interfaces;
using BLL.Service.Dto;
using DBL.Data;
using DBL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BLL.Service
{
    public class SpecialtyService : ISpecialtyService
    {
       
        private readonly AppDbContext _context;
        public SpecialtyService(AppDbContext context)
        {
           
            _context = context;
        }
        public SpecialtyDto Create(CreateSpecialtyDto dto)
        {
            if (IsNameExist(dto.Name))
            {
                throw new Exception("Specialty already exists.");
            }
            var specialty = new Specialty
            {
                Name = dto.Name,
                Code = dto.Code,
                IsActive = dto.IsActive,
                IsDelete = false,
            };
            _context.Specialties.Add(specialty);
            _context.SaveChanges();
            return GetById(specialty.Id);
        }

        public bool Delete(int id)
        {
            if (IsDoctorConnected(id))
            {
                throw new Exception("There is a doctor available in this specialty.");
            }
            var specialty = _context.Specialties.FirstOrDefault(x => x.Id == id && !x.IsDelete);
            if (specialty == null)
                return false;
            specialty.IsDelete = true;
            _context.SaveChanges();
            return true;
        }

        public List<SpecialtyDto> GetActive()
        {
            var specialty = _context.Specialties
               .Where(x => !x.IsActive)
               .Select(x => new SpecialtyDto
               {
                   Id = x.Id,
                   Name = x.Name,
                   IsActive = x.IsActive,
                   IsDelete = x.IsDelete,
               }).ToList();
            return specialty;
        }

        public List<SpecialtyDto> GetAll(string? Name, string? Code, bool? IsActive)
        {
            var specialty = _context.Specialties
                .Where(x => !x.IsDelete);
            if (!string.IsNullOrWhiteSpace(Name))
            {
                specialty = specialty.Where(x => x.Name.Contains(Name));
            }
            if (!string.IsNullOrWhiteSpace(Code))
            {
                specialty = specialty.Where(x => x.Code.Contains(Code));
            }
            if (IsActive == true)
            {
                specialty = specialty.Where(x => x.IsActive == true);
            }
            else if (IsActive == false)
            {
                specialty = specialty.Where(x => x.IsActive == false);
            }
            return specialty.Select(x => new SpecialtyDto
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                IsActive = x.IsActive,
                IsDelete = x.IsDelete,
            }).ToList();


        }

        public SpecialtyDto GetById(int id)
        {
            return _context.Specialties
                 .Where(x => x.Id == id && !x.IsDelete)
                .Select(x => new SpecialtyDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    IsActive = x.IsActive,
                    IsDelete = x.IsDelete,
                }).FirstOrDefault();
        }

        public bool Update(int id, UpdateSpecialtyDto dto)
        {
            if (IsNameExist(dto.Name, id))
            {
                throw new Exception("Specialty already exists.");
            }
          
            var specialty = _context.Specialties.FirstOrDefault(x => x.Id == id && !x.IsDelete);
            if (specialty == null) return false;
            specialty.Name = dto.Name;
            specialty.Code = dto.Code;
            if (specialty.IsActive && !dto.IsActive)
            {
                if (IsDoctorConnected(id))
                {
                    throw new Exception("There is a doctor available in this specialty.");
                }
            }
            _context.SaveChanges();
            return true;
        }
        private bool IsNameExist(string name, int? id = null)
        {
            var result = _context.Specialties.Where(x => x.Name == name && !x.IsDelete).Any();
            if (result)
            {
                if (id.HasValue)
                {
                    var found = _context.Specialties.Where(x => x.Name == name && x.Id != id).Any();
                    return found;
                }
                return result;
            }
            return result;
        }


        private bool IsDoctorConnected(int id)
        {
            var result = _context.Doctors.Where(x => x.SpecialtyId == id).Any();
          
            return result;
        }
      
    }
}
