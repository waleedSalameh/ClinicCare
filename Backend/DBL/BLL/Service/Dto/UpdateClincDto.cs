using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service.Dto
{
    public class UpdateClincDto
    {
        public string ClincName { get; set; }
        public string Specialization { get; set; }
        public int DoctorNo { get; set; }
        public int NumberOfPatients { get; set; }
        public bool IsActive { get; set; }
        public string Code { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
