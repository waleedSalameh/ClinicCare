using DBL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL.Entities
{
    public class Appointment
    {
        public int Id { get; set; } 
        public string AppointmentCode { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int ClincId { get; set; }
        public int MedicalServiceId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public AppointmentStatus Status { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public Doctor Doctor { get; set; }
        public Clinc Clinc { get; set; }
        public Patient Patient { get; set; }
        public MadicalService MadicalService { get; set; }

    }
}
