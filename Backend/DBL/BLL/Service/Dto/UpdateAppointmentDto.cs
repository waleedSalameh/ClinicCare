using DBL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service.Dto
{
    public class UpdateAppointmentDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int ClincId { get; set; }
        public int MedicalServiceId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public AppointmentStatus Status { get; set; }
        public string Notes { get; set; }
    }
}
