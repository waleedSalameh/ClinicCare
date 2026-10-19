using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL.Entities
{
    public class MadicalService
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceCode { get; set; }
        public decimal Price { get; set; }
        public string Desription {  get; set; }
        public int IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int DurationInMinutes { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int Clincid { get; set; } 
        public Clinc Clinc { get; set; }
        public ICollection<Appointment> Appointments { get; set; }




    }
}
