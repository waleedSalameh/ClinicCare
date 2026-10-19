using BLL.Interfaces;
using BLL.Service.Dto;
using DBL.Data;
using DBL.Entities;
using DBL.Enum;
using DBL.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BLL.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly AppDbContext _context;
        public AppointmentService(AppDbContext context)
        {
            _context = context;
        }
        public bool Change(int id, ChangeAppointmentStatusDto dto)
        {
            var query = _context.Appointments
                    .FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (query == null)
                return false;
            if (IsAppointmentPatient(query.PatientId, query.AppointmentDate, id))
            {
                throw new Exception("There is an appointment available for the patient on the same day.");
            }

            if (dto.Status == AppointmentStatus.NoShow)
            {
                if (IsNoShow(id))
                {
                    throw new Exception("The scheduled date has nott yet arrived.");
                }
            }
            if(dto.Status == AppointmentStatus.Booked) 
            {
                if (IsChange(id))
                {
                    throw new Exception("The status cannot be changed to booked .");
                }

            }

            if (query.Status == AppointmentStatus.Cancelled && dto.Status != AppointmentStatus.Cancelled)
            {
                bool exists = IsAppointmentStatus(id, query.DoctorId, query.AppointmentDate, query.StartTime, query.EndTime);

                if (exists)
                    return false;
            }


            query.Status = dto.Status;

            _context.SaveChanges();

            return true;
        }
        public AppointmentDto Create(CreateAppointmentDto dto)
        {

            var lastCode = _context.Appointments
                                      .OrderByDescending(x => x.AppointmentCode)
                                      .Select(x => x.AppointmentCode)
                                      .FirstOrDefault();
            int nextCode = 1;
            if (IsAppointmentPatient(dto.PatientId, dto.AppointmentDate))
            {
                throw new Exception("There is an appointment available for the patient on the same day.");
            }
            if (IsInWorkingHours(dto.DoctorId, dto.AppointmentDate.DayOfWeek, dto.StartTime, dto.EndTime))
            {
                throw new Exception(
                    "Appointment time is outside doctor's working hours.");
            }

            if (!string.IsNullOrEmpty(lastCode))
            {
                nextCode = int.Parse(lastCode) + 1;
            }
            if (IsAppointmentExists(dto.DoctorId, dto.AppointmentDate, dto.StartTime, dto.EndTime))
            {
                throw new Exception("There is an appointment at the same time.");
            }
            if (dto.EndTime <= dto.StartTime)
            {
                throw new Exception("EndTime must be greater than StartTime");
            }
            if (dto.AppointmentDate < DateTime.Today)
            {
                throw new Exception("The appointments are from an old date.");
            }


            var query = new DBL.Entities.Appointment
            {
                AppointmentDate = dto.AppointmentDate,
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                ClincId = dto.ClincId,
                MedicalServiceId = dto.MedicalServiceId,
                Notes = dto.Notes,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                IsDeleted = false,
                AppointmentCode = nextCode.ToString(),
                Status = dto.Status,
                CreatedAt = DateTime.Now,
            };
            _context.Appointments.Add(query);
            _context.SaveChanges();
            return GetById(query.Id);

        }
        public bool Delete(int id)
        {
            var query = _context.Appointments
                 .FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (query == null)
                return false;
            if (IsCancelld(id))
            {
                throw new Exception("To delete, it must be cancelled.");

            }
            query.IsDeleted = true;


            _context.SaveChanges();

            return true;
        }
        public List<AppointmentDto> GetAll(DateTime? FromDate, DateTime? ToDate, int? DoctorId, int? PatientId, int? ClincId, AppointmentStatus? Status)
        {
            var query = _context.Appointments.Where(x => !x.IsDeleted);


            if (FromDate.HasValue)
            {
                query = query.Where(x => x.AppointmentDate >= FromDate.Value);
            }

            if (ToDate.HasValue)
            {
                query = query.Where(x => x.AppointmentDate <= ToDate.Value);
            }

            if (DoctorId.HasValue)
            {
                query = query.Where(x => x.DoctorId == DoctorId.Value);
            }

            if (PatientId.HasValue)
            {
                query = query.Where(x => x.PatientId == PatientId.Value);
            }

            if (ClincId.HasValue)
            {
                query = query.Where(x => x.ClincId == ClincId.Value);
            }

            if (Status.HasValue)
            {
                query = query.Where(x => x.Status == Status.Value);
            }

            return query.Select(x => new AppointmentDto
            {
                Id = x.Id,
                AppointmentCode = x.AppointmentCode,
                PatientId = x.PatientId,
                DoctorId = x.DoctorId,
                ClincId = x.ClincId,
                MedicalServiceId = x.MedicalServiceId,
                PatientName = x.Patient.FullName,
                DoctorName = x.Doctor.FullName,
                ClincName = x.Clinc.ClincName,
                ServiceName = x.MadicalService.ServiceName,
                AppointmentDate = x.AppointmentDate,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Status = x.Status,
                Notes = x.Notes,
                CreatedAt = x.CreatedAt
            }).ToList();
        }
        public AppointmentDto GetById(int id)
        {
            var query = _context.Appointments.Where(x => x.Id == id && !x.IsDeleted)
                .Select(x => new AppointmentDto
                {
                    Id = x.Id,
                    AppointmentCode = x.AppointmentCode,
                    PatientId = x.PatientId,
                    DoctorId = x.DoctorId,
                    ClincId = x.ClincId,
                    MedicalServiceId = x.MedicalServiceId,
                    AppointmentDate = x.AppointmentDate,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    Status = x.Status,
                    Notes = x.Notes,
                    CreatedAt = x.CreatedAt,
                    PatientName = x.Patient.FullName,
                    DoctorName = x.Doctor.FullName,
                    ClincName = x.Clinc.ClincName,
                    ServiceName = x.MadicalService.ServiceName,
                }).FirstOrDefault();
            return query;

        }
        public bool Update(int id, UpdateAppointmentDto dto)
        {
            var query = _context.Appointments
                .FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (IsAppointmentPatient(dto.PatientId, dto.AppointmentDate, id))
            {
                throw new Exception("There is an appointment available for the patient on the same day.");
            }
            if (IsInWorkingHours(dto.DoctorId, dto.AppointmentDate.DayOfWeek, dto.StartTime, dto.EndTime))
            {
                throw new Exception(
                    "Appointment time is outside doctor's working hours.");
            }
            if (IsAppointmentExists(dto.DoctorId, dto.AppointmentDate, dto.StartTime, dto.EndTime, id))
            {
                throw new Exception("There is an appointment at the same time.");
            }
            if (query == null)
                return false;
            if (dto.EndTime <= dto.StartTime)
            {
                throw new Exception("EndTime must be greater than StartTime");
            }
            if (dto.AppointmentDate < DateTime.Today)
            {
                throw new Exception("The appointments are from an old date.");
            }

            query.DoctorId = dto.DoctorId;
            query.PatientId = dto.PatientId;
            query.MedicalServiceId = dto.MedicalServiceId;
            query.ClincId = dto.ClincId;
            query.AppointmentDate = dto.AppointmentDate;
            query.StartTime = dto.StartTime;
            query.EndTime = dto.EndTime;
            query.Notes = dto.Notes;
            _context.SaveChanges();
            return true;
        }
        private bool IsAppointmentPatient(int patient, DateTime AppointmentDate, int? id = null)
        {
            var result = _context.Appointments.Where(x => x.PatientId == patient && x.AppointmentDate == AppointmentDate && !x.IsDeleted && x.Id != id && x.Status != AppointmentStatus.Cancelled).Any();
            return result;
        }
        private bool IsAppointmentExists(int doctorId, DateTime AppointmentDate, TimeSpan startTime, TimeSpan endTime, int? id = null)
        {
            var result = _context.Appointments.Where(x => x.DoctorId == doctorId && x.AppointmentDate == AppointmentDate &&
            !x.IsDeleted && (startTime < x.EndTime && endTime > x.StartTime) && x.Id != id && x.Status != AppointmentStatus.Cancelled).Any();
            return result;
        }
        private bool IsInWorkingHours(int id, DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
        {

            var result = _context.DoctorSchedules.Where(x => x.DoctorId == id && x.DayOfWeek == dayOfWeek && (x.StarTime <= startTime && x.EndTime >= endTime)).Any();
            return !result;
        }
        private bool IsAppointmentStatus(int id, int doctorId, DateTime appointmentDate, TimeSpan startTime, TimeSpan endTime)
        {
            return _context.Appointments.Where(x => x.Id != id && x.DoctorId == doctorId && x.AppointmentDate == appointmentDate && !x.IsDeleted && x.Status != AppointmentStatus.Cancelled && x.StartTime < endTime && x.EndTime > startTime).Any();
        }
        private bool IsCancelld(int id)
        {
            var result = _context.Appointments.Where(x => x.Id == id && x.Status == AppointmentStatus.Cancelled).Any();
            return !result;
        }
        private bool IsNoShow(int id)
        {
            var result = _context.Appointments.Where(x => x.Id == id).FirstOrDefault();
            if (result.AppointmentDate >= DateTime.Today && result.StartTime <= DateTime.Now.TimeOfDay)
            {
                return true;
            }
            return false;
        }
        private bool IsChange(int id) 
        {
            var result = _context.Appointments.Where(x => x.Id == id && x.Status == AppointmentStatus.Confirmed).Any();
            return result;
        
        
        }
        public List<AppointmentDto> BookingAppointments(int clincId, int MSId, int DoctorId, DateTime AppointmentDate)
        {

            var clinc = _context.Clincs.FirstOrDefault(x => x.Id == clincId && !x.IsDeleted && x.IsActive);
            var doctor = _context.DoctorSchedules.FirstOrDefault(x => x.DoctorId == DoctorId && !x.IsDeleted);
            var MS = _context.MadicalServices.FirstOrDefault(x => x.Id == MSId);
            var Appointment = _context.Appointments.Where(x => x.DoctorId == DoctorId && x.ClincId == clincId
            && x.AppointmentDate == AppointmentDate && !x.IsDeleted && x.Status != AppointmentStatus.Cancelled).OrderBy(x => x.StartTime).ToList();
            TimeSpan startTime = doctor.StarTime > clinc.StartTime ? doctor.StarTime : clinc.StartTime;
            TimeSpan endTime = doctor.EndTime < clinc.EndTime ? doctor.EndTime : clinc.EndTime;
            TimeSpan duration = TimeSpan.FromMinutes(MS.DurationInMinutes);

            //if (Appointment == null)
            //{
            //    if (doctor.StarTime >= clinc.StartTime) { startTime = doctor.StarTime; } else { startTime = clinc.StartTime; }
            //    endTime=startTime + duration;
            //}
            var AppointmentList = new List<AppointmentDto>();

            for (TimeSpan time = startTime; time + duration <= endTime; )
            {
                bool available = true;
                foreach (var appointment in Appointment)
                {

                    if (time + duration  > appointment.StartTime&& appointment.EndTime > time)
                    {
                        available = false;
                        //AppointmentList.Add(new AppointmentDto
                        //{
                        //    StartTime = time,
                        //    EndTime = time + duration
                        //});
                        time = appointment.EndTime;
                        break;

                    }

                  
                }
                if (available)
                {
                    AppointmentList.Add(new AppointmentDto
                    {
                        StartTime = time,
                        EndTime = time + duration
                    });
                    //return AppointmentList;
                    time += duration;
                }
               
            }
            return AppointmentList;


        }

    }
}
