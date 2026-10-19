using DBL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service.Dto
{
    public class AppointmentDto
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
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string ClincName { get; set; }
        public string ServiceName { get; set; }


    }
}
