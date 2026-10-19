
using BLL.Interfaces;
using DBL.Entities;
using DBL.Data;
using BLL.Service.Dto;
using BLL.Interfaces.BLL.Interfaces;

namespace BLL.Service
{


    public class DoctorService : IDoctorService
    {
        private readonly AppDbContext _context;

        public DoctorService(AppDbContext context)
        {
            _context = context;
        }


        public List<DoctorDto> GetAll(string? fullName,string? phoneNumber,int? specialtyId)
        {
            var query =
                from doctor in _context.Doctors

                join specialty in _context.Specialties
                    on doctor.SpecialtyId equals specialty.Id

                where  !doctor.IsDeleted

                select new
                {
                    doctor,
                    specialty
                };

            if (!string.IsNullOrWhiteSpace(fullName))
            {
                query = query.Where(x =>
                    x.doctor.FullName.Contains(fullName));
            }

            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                query = query.Where(x =>
                    x.doctor.PhoneNumber.Contains(phoneNumber));
            }

            if (specialtyId.HasValue)
            {
                query = query.Where(x =>
                    x.doctor.SpecialtyId == specialtyId.Value);
            }

            return query
                .Select(x => new DoctorDto
                {
                    Id = x.doctor.Id,
                    FullName = x.doctor.FullName,
                    SpecialtyId = x.doctor.SpecialtyId,
                    SpecialtyName = x.specialty.Name,
                    PhoneNumber = x.doctor.PhoneNumber,
                    YearsOfExperience = x.doctor.YearsOfExperience,
                    Email = x.doctor.Email,
                    Gender = x.doctor.Gender,
                    IsActive = x.doctor.IsActive,
                    CreatedDateTime = x.doctor.CreatedDate.Add(x.doctor.CreatedTime),
                    IsDeleted = x.doctor.IsDeleted,
                })
                .ToList();
        }
        public DoctorDto? GetById(int id)
        {
            return _context.Doctors
                .Where(x => x.Id == id&&!x.IsDeleted)
                .Select(x => new DoctorDto
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    SpecialtyId = x.SpecialtyId,
                    SpecialtyName = x.Specialty.Name,

                    PhoneNumber = x.PhoneNumber,

                    YearsOfExperience =
                        x.YearsOfExperience,

                    Email = x.Email,

                    Gender = x.Gender,

                    IsActive = x.IsActive,

                    CreatedDateTime =x.CreatedDate.Add(x.CreatedTime),
                    IsDeleted = x.IsDeleted

                })
                .FirstOrDefault();
        }

        public DoctorDto Create(CreateDoctorDto dto)
        {
            var doctor = new Doctor
            {
                FullName = dto.FullName,

                SpecialtyId = dto.SpecialtyId,

                PhoneNumber = dto.PhoneNumber,

                YearsOfExperience =
                    dto.YearsOfExperience,

                Email = dto.Email,

                Gender = dto.Gender,

                IsActive = dto.IsActive,

                CreatedDate = dto.CreatedDateTime.Date,

                CreatedTime = dto.CreatedDateTime.TimeOfDay,
                IsDeleted = false

            };

            _context.Doctors.Add(doctor);

            _context.SaveChanges();

            return GetById(doctor.Id)!;
        }

        public bool Update(int id,UpdateDoctorDto dto)
        {
            var doctor = _context.Doctors
                .FirstOrDefault(x => x.Id == id && !x.IsDeleted );
            
            if (doctor == null)
                return false;

            doctor.FullName =
                dto.FullName;

            doctor.SpecialtyId =
                dto.SpecialtyId;

            doctor.PhoneNumber =
                dto.PhoneNumber;

            doctor.YearsOfExperience =
                dto.YearsOfExperience;

            doctor.Email =
                dto.Email;

            doctor.Gender =
                dto.Gender;
            if (doctor.IsActive&& !dto.IsActive)
            {
                if (IsDoctorConnected(id))
                {
                    throw new Exception("Doctor connected to other records.");
                }
            }
            doctor.IsActive = dto.IsActive;
            doctor.CreatedDate =
                dto.CreatedDateTime.Date;

            doctor.CreatedTime =
                dto.CreatedDateTime.TimeOfDay;

            _context.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var doctor = _context.Doctors
                .FirstOrDefault(x => x.Id == id&&!x.IsDeleted);

            if (IsDoctorConnected(id))
            {
                throw new Exception("Doctor connected to other records.");
            }
            if (doctor == null)
                return false;
            doctor.IsDeleted =true;

            _context.SaveChanges();

            return true;
        }
        public List<SpecialtyDto> GetSpecialties()
        {
            return _context.Specialties
                .Where(x => x.IsActive&&!x.IsDelete)
                .Select(x => new SpecialtyDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    IsActive = x.IsActive
                })
                .ToList();
        }
        private bool IsDoctorConnected(int id)
        {
            var result = _context.Clincs.Where(x => x.DoctorNo == id).Any()
                ||_context.Appointments.Where(x=>x.DoctorId == id).Any();
          
            return result;
        }
        private bool IsInWorkingHours(int id, DayOfWeek dayOfWeek,TimeSpan startTime,TimeSpan endTime)
        {
            var result=_context.DoctorSchedules.Where(x=>x.DoctorId==id&&x.DayOfWeek==dayOfWeek&&(x.StarTime<=startTime&&x.EndTime>=endTime)).Any ();
            return result;
        }

    }
}