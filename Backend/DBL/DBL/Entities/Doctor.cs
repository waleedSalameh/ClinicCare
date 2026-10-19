using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL.Entities
{
    public class Doctor
    {
        public int Id { get; set; } 
        public string FullName { get; set; }
        public int SpecialtyId {  get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive {  get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int YearsOfExperience { get; set; }
        public Specialty Specialty { get; set; }
        public DateTime CreatedDate { get; set; }
        public TimeSpan CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Clinc> Clincs { get; set; }
        public ICollection<DoctorSchedule> DoctorSchedules { get; set; }
         public ICollection<Appointment> Appointments { get; set; }
    }




}

