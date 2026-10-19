using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service.Dto
{
    public class ClincDto
    {
        public int Id { get; set; }
        public int ClincCode { get; set; }
        public string ClincName { get; set; }
        public string Specialization { get; set; }
        public int DoctorNo { get; set; }
        public string DoctorName { get; set; }
        public int NumberOfPatients { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string Code { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

    }
}
