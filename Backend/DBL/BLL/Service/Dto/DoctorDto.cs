using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service.Dto
{
    public class DoctorDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public int SpecialtyId { get; set; }

        public string SpecialtyName { get; set; }

        public string PhoneNumber { get; set; }

        public int YearsOfExperience { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public bool IsDeleted { get; set; }

    }
}