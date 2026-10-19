using BLL.Interfaces;
using BLL.Service.Dto;
using DBL.Data;
using DBL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;
        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public object  GetClincByAppointmets()
        {
            var todayAppointment = _context.Appointments.Where(x => x.AppointmentDate == DateTime.Today && !x.IsDeleted);
            return todayAppointment.GroupBy(x=> new
            {
                x.ClincId,
                clincName=x.Clinc.ClincName,
            }).Select(x =>new {
              clincId=x.Key.ClincId,
              clincName=x.Key.clincName,
              AppointmentCount=x.Count()
               
            })
            .ToList();
        }

        public DashboardSummaryDto GetSummary()
        {
            var todayAppointment = _context.Appointments.Where(x => x.AppointmentDate == DateTime.Today && !x.IsDeleted);
            return new DashboardSummaryDto
            {
                TodayAppointmentsCount = todayAppointment.Count(),
                BookedCount = todayAppointment.Count(x => x.Status == AppointmentStatus.Booked),
                CompletedCount = todayAppointment.Count(x => x.Status == AppointmentStatus.Completed),
                CancelledCount = todayAppointment.Count(x => x.Status == AppointmentStatus.Cancelled)

            };

        }
        public List<TodayAppointmentDto> GetTodayAppointments()
        {
           return _context.Appointments.Where(x=>x.AppointmentDate==DateTime.Today&&!x.IsDeleted&&x.Status!= AppointmentStatus.Cancelled).
                OrderBy(x=>x.StartTime).Select(x=>new TodayAppointmentDto
                {
                    AppointmentId=x.Id,
                    AppointmentTime=x.StartTime,
                    ClincName=x.Clinc.ClincName,
                    PatientName=x.Patient.FullName,
                    DoctorName=x.Doctor.FullName,
                    Status=x.Status

                }).ToList();
        }
    }
}
