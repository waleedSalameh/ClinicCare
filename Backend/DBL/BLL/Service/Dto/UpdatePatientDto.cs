using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service.Dto
{
    public class UpdatePatientDto
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string NationalId { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required]
        public int Gender { get; set; }
        public string? Address { get; set; }
    }
}
