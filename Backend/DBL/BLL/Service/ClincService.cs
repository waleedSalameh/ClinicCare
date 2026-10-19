using BLL.Interfaces;
using BLL.Service.Dto;
using DBL.Data;
using DBL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BLL.Service
{
    public class ClincService:IClincService
    {
        private readonly AppDbContext _context;
        public ClincService(AppDbContext context)
        {
            _context = context;
        }

        public ClincDto Create(CreateClincDto dto)
        {
            var lastCode = _context.Clincs
                          .OrderByDescending(x => x.ClincCode)
                          .Select(x => x.ClincCode)
                          .FirstOrDefault();
            var Clinc = new Clinc
            {
                ClincName = dto.ClincName,
                Specialization = dto.Specialization,
                DoctorNo = dto.DoctorNo,
                NumberOfPatients = dto.NumberOfPatients,
                IsActive = dto.IsActive,
                Code = dto.Code,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                ClincCode = lastCode + 1,
            };
            _context.Clincs.Add(Clinc);
            _context.SaveChanges();
            return GetById(Clinc.Id);


        }

        public bool Delete(int id)
        {
            var clinc = _context.Clincs
             .FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (IsClinicConnected(id))
            {
                throw new Exception("Clinic connected to other records.");
            }

            if (clinc == null)
                return false;

            clinc.IsDeleted = true;

            _context.SaveChanges();

            return true;
        }

        public List<ClincDto> GetAll(string? ClincName,string?ClincCode,bool? IsActive)
        {
            var query =
                from clinc in _context.Clincs

                join doctor in _context.Doctors
                    on clinc.DoctorNo equals doctor.Id

                where !clinc.IsDeleted

                select new
                {
                    clinc,
                    doctor
                };
            

            if (!string.IsNullOrWhiteSpace(ClincName))
            {
                query = query.Where(x =>
                    x.clinc.ClincName.Contains(ClincName));
            }

            if (!string.IsNullOrWhiteSpace(ClincCode))
            {
                query = query.Where(x =>
                    x.clinc.ClincName.ToString().Contains(ClincCode));
            }
           

            if (IsActive == true)
            {
                query = query.Where(x => x.clinc.IsActive == true);
            }else if (IsActive == false)
            {
                query = query.Where(x=>x.clinc.IsActive == false);
            }
            
         return query.Select(x => new ClincDto
                {
                    Id = x.clinc.Id,
                    ClincName = x.clinc.ClincName,
                    Specialization = x.clinc.Specialization,
                    DoctorNo = x.clinc.DoctorNo,
                    DoctorName = x.doctor.FullName,
                    NumberOfPatients = x.clinc.NumberOfPatients,
                    ClincCode= x.clinc.ClincCode,
                    IsActive= x.clinc.IsActive,
                    IsDeleted = x.clinc.IsDeleted,
                    Code = x.clinc.Code,
                    StartTime = x.clinc.StartTime,
                    EndTime= x.clinc.EndTime,

                }).ToList();

             
        }

        public ClincDto GetById(int id)
        {
            var Clinc = _context.Clincs.Where(x => x.Id == id && !x.IsDeleted)
                  .Select(x => new ClincDto
                  {
                      Id = x.Id,
                      ClincName = x.ClincName,
                      Specialization = x.Specialization,
                      DoctorNo = x.DoctorNo,
                      NumberOfPatients = x.NumberOfPatients,
                      ClincCode = x.ClincCode,
                      IsActive = x.IsActive,
                      IsDeleted = x.IsDeleted,
                      Code = x.Code,
                      StartTime=x.StartTime,
                      EndTime=x.EndTime,


                  }).FirstOrDefault();

            return Clinc;

        }

        public bool Update(int id, UpdateClincDto dto)
        {
            var Clinc = _context.Clincs.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

           

            if (Clinc== null) return false;
            Clinc.ClincName = dto.ClincName;
            Clinc.DoctorNo = dto.DoctorNo;
            Clinc.NumberOfPatients = dto.NumberOfPatients;
            Clinc.Specialization = dto.Specialization;
            if(Clinc.IsActive&&!dto.IsActive)
            {
                if (IsClinicConnected(id))
                {
                    throw new Exception("Clinic connected to other records.");
                }
            }
            Clinc.IsActive = dto.IsActive;
            Clinc.Code = dto.Code;
            Clinc.StartTime= dto.StartTime;
            Clinc.EndTime= dto.EndTime;
            _context.SaveChanges();
            return true;
        }



        private bool IsClinicConnected(int id)
        {
            var result = _context.MadicalServices.Where(x => x.Clincid == id).Any()
                || _context.Appointments.Where(x => x.ClincId == id).Any();
            return result;
        }
    }
}
