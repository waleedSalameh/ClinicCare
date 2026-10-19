using BLL.Service.Dto;
using DBL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPatientService
    {
        List<PatientDto> GetAll(string? FullName, string? PhoneNumber, string? NationalId);
        PatientDto? GetById(int id);
        PatientDto Create(CreatePatientDto dto);
        bool Update(int id,UpdatePatientDto dto);
        bool Delete(int id);



    }
}
