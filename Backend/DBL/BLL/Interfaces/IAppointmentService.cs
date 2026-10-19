using BLL.Service.Dto;
using DBL.Enum;
using DBL.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAppointmentService
    {
        List<AppointmentDto> GetAll(DateTime? FromDate, DateTime? ToDate,int? DoctorId,int? PatientId,int? ClincId, AppointmentStatus? Status);
        AppointmentDto GetById(int id);
        AppointmentDto Create(CreateAppointmentDto dto);
        bool Update(int id, UpdateAppointmentDto dto);
        bool Delete(int id);
        bool Change(int id, ChangeAppointmentStatusDto dto);
       List <AppointmentDto> BookingAppointments(int clincId, int MSId, int DoctorId, DateTime AppointmentDate);
    }
}
