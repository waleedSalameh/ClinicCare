using BLL.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
   public interface ISpecialtyService
    {
        List<SpecialtyDto> GetAll(string? Name, string? Code, bool? IsActive);
        List<SpecialtyDto> GetActive();
        SpecialtyDto GetById(int id);
        SpecialtyDto Create(CreateSpecialtyDto dto);
        bool Update(int id, UpdateSpecialtyDto dto);
        bool Delete(int id);

    }
}
