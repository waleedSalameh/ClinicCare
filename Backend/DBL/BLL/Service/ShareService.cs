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
    public class ShareService : IShareService
    {
        private readonly AppDbContext _context;

        public ShareService(AppDbContext context)
        {
            _context = context;
        }

        public List<ClincDto> GetClincs()
        {
            var query = _context.Clincs
                .Where(x => !x.IsDeleted&& x.IsActive)
                .Select(x => new ClincDto
                {
                    Id = x.Id,
                    ClincName = x.ClincName,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                }).ToList();
            return query;
        }
        public List<PatientDto> GetPatients()
        {
            var query = _context.Patients
                .Where(x => !x.IsDeleted)
                .Select(x => new PatientDto
                {
                    Id = x.Id,
                    FullName = x.FullName,
                }).ToList();
            return query;

        }
        public List<DoctorDto> GetDoctors()
        {
            var query = _context.Doctors
                 .Where(x => !x.IsDeleted)
                 .Select(x=> new DoctorDto
                 {
                     Id = x.Id,
                     FullName = x.FullName,
                 }).ToList();
            return query;

        }
        public List<DoctorDto> GetDoctorsByClinic(int clinicId)
        {
            var Doctor= _context.Doctors
                .Where(x =>x.IsActive && !x.IsDeleted && x.Clincs.Any(c => c.Id == clinicId && !c.IsDeleted))
                .Select(x => new DoctorDto
                {
                    Id = x.Id,
                    FullName = x.FullName
                })
                .ToList();
            if (!Doctor.Any())
            {
                throw new Exception("Doctor not found.");
            }

            return Doctor;

        }
        public List<MedicalServiceDto> GetMSByClinic(int clinicId)
        {
            var MadicalService= _context.MadicalServices
                .Where(x =>  x.Clincid == clinicId && !x.IsDeleted && x.IsActive == 1) 
                .Select(x => new MedicalServiceDto
                {
                    Id = x.Id,
                    ServiceName = x.ServiceName,
                    DurationInMinutes = x.DurationInMinutes,
                })
                .ToList();
            if (!MadicalService.Any())
            {
                throw new Exception("Medical service not found.");
            }

            return MadicalService;
        }
        public string ValidPhoneNumber(string phoneNumber)
        {
            if (phoneNumber == "" || phoneNumber == null)
                return "number is Required ";
            if (phoneNumber.Length != 10)
                return "number ";
            if (phoneNumber[0] != '0' || phoneNumber[1] != '7')
                return "number 07";





            char[] num = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            for (int i = 0; i < phoneNumber.Length; i++)
            {
                for (int j = 0; j < num.Length; j++)
                {
                    if (phoneNumber[i] == num[j])
                        break;
                    else if (num.Length-1==j )
                    {
                        return "number";
                    }
                  
                }

            }
            return "";
        }

      
    }
}
