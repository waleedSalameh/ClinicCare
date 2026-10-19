using BLL.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDoctorScheduleService
    {
        List<DoctorScheduleDto> GetFilter(int?DoctorId,DayOfWeek?DayOfWeek);
        DoctorScheduleDto Create(CreateDoctorScheduleDto dto);
        DoctorScheduleDto GetById(int id);
        bool Delete (int id);


    }
}
