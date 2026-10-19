using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service.Dto
{
    public class CreateDoctorDto
    {
        [Required]
        public string FullName { get; set; }

        public int SpecialtyId { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public int YearsOfExperience { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Gender { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}