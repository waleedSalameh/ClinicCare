using BLL.Interfaces;
using BLL.Service.Dto;
using DBL.Data;
using DBL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class DoctorScheduleService : IDoctorScheduleService
    {
        private readonly AppDbContext _context;
        public DoctorScheduleService(AppDbContext context)
        {
            _context = context;
        }

        public DoctorScheduleDto Create(CreateDoctorScheduleDto dto)
        {
            if (IsAppointmentExists(dto.DoctorId, dto.DayOfWeek))
            {
                throw new Exception("There is a shift available on the same day..");
            }
            if (dto.EndTime <= dto.StarTime)
            {
                throw new Exception("EndTime must be greater than StartTime");
            }
            var doctor = new DoctorSchedule
            {
                DoctorId = dto.DoctorId,
                DayOfWeek = dto.DayOfWeek,
                StarTime = dto.StarTime,
                EndTime = dto.EndTime,
                SlotDurationMinutes = dto.SlotDurationMinutes,

            };
            _context.DoctorSchedules.Add(doctor);
            _context.SaveChanges();
            return GetById(doctor.Id);
        }

        public bool Delete(int id)
        {
            var Doc = _context.DoctorSchedules
                 .FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (Doc == null)
                return false;

            Doc.IsDeleted = true;

            _context.SaveChanges();

            return true;
        }

        public DoctorScheduleDto GetById(int id)
        {
            return _context.DoctorSchedules
                 .Where(x => x.Id == id && !x.IsDeleted)
                .Select(x => new DoctorScheduleDto
                {
                    Id = x.Id,
                    DoctorId = x.DoctorId,
                    StarTime = x.StarTime,
                    EndTime = x.EndTime,
                    DayOfWeek = x.DayOfWeek,
                    IsDeleted = x.IsDeleted,
                    SlotDurationMinutes = x.SlotDurationMinutes,
                }).FirstOrDefault();
        }

        public List<DoctorScheduleDto> GetFilter(int? DoctorId, DayOfWeek? DayOfWeek)
        {
            var query =
                from doctorSchedule in _context.DoctorSchedules

                join doctor in _context.Doctors
                    on doctorSchedule.DoctorId equals doctor.Id

                where !doctorSchedule.IsDeleted

                select new
                {
                    doctorSchedule,
                    doctor
                };

            if (DoctorId.HasValue)
            {
                query = query.Where(x =>
                    x.doctorSchedule.DoctorId == DoctorId.Value);
            }
            if (DayOfWeek.HasValue)
            {
                query = query.Where(x =>
                    x.doctorSchedule.DayOfWeek == DayOfWeek.Value);
            }
            return query.Select(x => new DoctorScheduleDto
            {
                Id=x.doctorSchedule.Id,
                DoctorId=x.doctorSchedule.DoctorId,
                DayOfWeek=x.doctorSchedule.DayOfWeek,
                StarTime=x.doctorSchedule.StarTime,
                EndTime=x.doctorSchedule.EndTime,
                IsDeleted=x.doctorSchedule.IsDeleted,
                DoctorName=x.doctor.FullName,
                SlotDurationMinutes=x.doctorSchedule.SlotDurationMinutes,
            }).ToList();





        }

        private bool IsAppointmentExists( int doctorId,DayOfWeek dayOfWeek) 
        {
            var result =_context.DoctorSchedules.Where(x=>x.DoctorId==doctorId && x.DayOfWeek==dayOfWeek&&!x.IsDeleted).Any();
          return result;
         }







        }




    }

