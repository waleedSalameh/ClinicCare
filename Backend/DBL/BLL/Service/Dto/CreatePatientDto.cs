using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service.Dto
{
    public class CreatePatientDto
    {
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "NationalId is required")]
        public string NationalId { get; set; }
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public int Gender { get; set; }
        public string? Address { get; set; }
    }
}
