using BLL.Service.Dto;
using DBL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    namespace BLL.Interfaces
    {
        public interface IDoctorService
        {
            List<DoctorDto> GetAll(string? fullName, string? phoneNumber, int? specialtyId);

            DoctorDto? GetById(int id);

            DoctorDto Create(CreateDoctorDto dto);

            bool Update(int id, UpdateDoctorDto dto);

            bool Delete(int id);
            List<SpecialtyDto> GetSpecialties();
        }

    }
}