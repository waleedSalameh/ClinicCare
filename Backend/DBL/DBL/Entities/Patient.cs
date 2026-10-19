using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL.Entities
{
    public class Patient
    {
      public int Id { get; set; }
      public int Code { get; set; }
      public string FullName { get; set; }
      public string NationalId { get; set; }
      public string PhoneNumber { get; set; }
      public DateTime DateOfBirth { get; set; }
      public int Gender { get; set; }
      public string? Address { get; set; }
      public bool IsDeleted { get; set; }
      public ICollection<Appointment> Appointments { get; set; }







    }
}
