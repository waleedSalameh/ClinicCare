using BLL.Interfaces;
using BLL.Service.Dto;
using DBL.Data;
using DBL.Entities;
using System.Linq;

namespace BLL.Service
{
    public class PatientService : IPatientService
    {
        private readonly AppDbContext _context;
        private readonly IShareService _ShareService;
        public PatientService(AppDbContext context, IShareService shareService)
        {
            _context = context;
            _ShareService = shareService;
        }
        public PatientDto Create(CreatePatientDto dto)
        {
            if (IsNationalIdExists(dto.NationalId))
            {
                throw new Exception("National ID already exists.");
            }
            var error = _ShareService.ValidPhoneNumber(dto.PhoneNumber);

            if (error != "")
            {
                throw new Exception(error);
            }

            var lastCode = _context.Patients
                          .OrderByDescending(x => x.Code)
                          .Select(x => x.Code)
                          .FirstOrDefault();



            var patient = new Patient
            {
                FullName = dto.FullName,
                Address = dto.Address,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                NationalId = dto.NationalId,
                PhoneNumber = dto.PhoneNumber,
                IsDeleted = false,
                Code = lastCode + 1
            };
            _context.Patients.Add(patient);
            _context.SaveChanges();
            return GetById(patient.Id);
        }

        public bool Delete(int id)
        {
            var patient = _context.Patients
                .FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (patient == null)
                return false;

            patient.IsDeleted = true;

            _context.SaveChanges();

            return true;
        }

        public List<PatientDto> GetAll(string? FullName, string? PhoneNumber, string? NationalId)
        {
            var query = _context.Patients
                  .Where(x => !x.IsDeleted);


            if (!string.IsNullOrWhiteSpace(FullName))
            {
                query = query.Where(x =>
                    x.FullName.Contains(FullName));
            }

            if (!string.IsNullOrWhiteSpace(PhoneNumber))
            {
                query = query.Where(x =>
                    x.PhoneNumber.Contains(PhoneNumber));
            }

            if (!string.IsNullOrWhiteSpace(NationalId))
            {
                query = query.Where(x =>
                    x.NationalId.Contains(NationalId));
            }

            var patients = query
          .Select(x => new PatientDto
          {
              Id = x.Id,
              Code = x.Code,
              FullName = x.FullName,
              NationalId = x.NationalId,
              PhoneNumber = x.PhoneNumber,
              DateOfBirth = x.DateOfBirth,
              Gender = x.Gender,
              Address = x.Address,
              IsDeleted = x.IsDeleted
          })
           .ToList();
            foreach (var patient in patients)
            {
                patient.Age = CalculateAge(patient.DateOfBirth);
            }
            return patients;

        }

        public PatientDto? GetById(int id)
        {
            var patient = _context.Patients.Where(x => x.Id == id && !x.IsDeleted)
                .Select(x => new PatientDto
                {
                    Id = x.Id,
                    Code = x.Code,
                    FullName = x.FullName,
                    NationalId = x.NationalId,
                    PhoneNumber = x.PhoneNumber,
                    DateOfBirth = x.DateOfBirth,
                    Gender = x.Gender,
                    Address = x.Address,
                    IsDeleted = x.IsDeleted
                })
                .FirstOrDefault();
            patient.Age = CalculateAge(patient.DateOfBirth);

            return patient;

        }

        public bool Update(int id, UpdatePatientDto dto)
        {
            if (IsNationalIdExists(dto.NationalId, id))
            {
                throw new Exception("National ID already exists.");
            }
            var error = _ShareService.ValidPhoneNumber(dto.PhoneNumber);

            if (error != "")
            {
                throw new Exception(error);
            }
            var patient = _context.Patients
                .FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (patient == null)
                return false;

            patient.FullName = dto.FullName;
            patient.NationalId = dto.NationalId;
            patient.PhoneNumber = dto.PhoneNumber;
            patient.DateOfBirth = dto.DateOfBirth;
            patient.Gender = dto.Gender;
            patient.Address = dto.Address;

            _context.SaveChanges();

            return true;
        }
        private string CalculateAge(DateTime dateOfBirth)
        {
            int[] daysInMonths = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            var today = DateTime.Today;
           

            if (dateOfBirth > today)
                return "Invalid date";
            int result = today.Year - dateOfBirth.Year;
            if (result > 1)
                return $"{result} years";
            else if (result <= 1)
            {
                int months = (today.Year - dateOfBirth.Year) * 12 + (today.Month - dateOfBirth.Month);
                if (months == 1 )
                {
                    int calDays = (today.Month - dateOfBirth.Month) * daysInMonths[dateOfBirth.Month] - (today - dateOfBirth.Date).Days;
                    int days = daysInMonths[dateOfBirth.Month] - calDays ;
                    return days <= daysInMonths[dateOfBirth.Month] ? $"{days} days" : $"1 months";

                }

                if (Convert.ToDouble(months)  == 0)
                {
                    int day =(today - dateOfBirth.Date).Days;
                    return $"{day} days";
                }

                return $"{months} months";
            }
         


            return "Invalid date";





        }
        private bool IsNationalIdExists(string nationalId, int? patientId = null)
        {
            var result = _context.Patients.Where(x=> x.NationalId == nationalId && !x.IsDeleted).Any();
            if (result)
            {
                if (patientId.HasValue)
                {
                    var IsFound = _context.Patients.Where(x=> x.NationalId == nationalId && x.Id != patientId.Value).Any();
                    return IsFound;
                }
                return result;
            }
            return result;
        }
    }
}

