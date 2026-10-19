using BLL.Service.Dto;
using DBL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMedicalServiceService
    {
        List<MedicalServiceDto> GatAll(string? ServiceName, string? ServiceCode, int? IsActive);
        MedicalServiceDto GetById(int id);
        MedicalServiceDto Create(CreateMedicalServiceDto dto);
        bool Update(int id, UpdateMedicalServiceDto dto);
        bool Delete(int id);


    }
}
