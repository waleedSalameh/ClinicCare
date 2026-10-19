using DBL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service.Dto
{
    public class TodayAppointmentDto
    {
        public int AppointmentId { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string PatientName { get; set; } 
        public string DoctorName { get; set; } 
        public string ClincName { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
