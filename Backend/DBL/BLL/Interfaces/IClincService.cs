using BLL.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IClincService
    {
       List<ClincDto> GetAll(string? ClincName,string?ClincCode,bool? IsActive);
       ClincDto GetById(int id);
       ClincDto Create(CreateClincDto dto);
       bool Update(int id,UpdateClincDto dto);
       bool Delete(int id);
    }
}
