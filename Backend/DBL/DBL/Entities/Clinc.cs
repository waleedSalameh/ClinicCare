using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL.Entities
{
    public class Clinc
    {
        public int Id { get; set; }
        public int ClincCode { get; set; }
        public string ClincName { get; set; }
        public string Specialization { get; set; }
        public int DoctorNo { get; set; }
        public int NumberOfPatients { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string Code { get; set; }
        public Doctor Doctor { get; set; }
        public ICollection<MadicalService> MadicalServices { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }


    }
}
