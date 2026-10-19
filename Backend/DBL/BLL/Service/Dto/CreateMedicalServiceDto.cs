using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service.Dto
{
    public class CreateMedicalServiceDto
    {
        [Required(ErrorMessage = "ServiceName is required")]
        public string ServiceName { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Only numbers are allowed")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Desription is required")]
        [StringLength(200, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Desription { get; set; }
        public int IsActive { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Duration must be greater than 0.")]
        public int DurationInMinutes { get; set; }
        [Required(ErrorMessage = "CostPrice is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal CostPrice { get; set; }
        [Required(ErrorMessage = "SellingPrice is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal SellingPrice { get; set; }
        public int Clincid { get; set; }


    }
}
