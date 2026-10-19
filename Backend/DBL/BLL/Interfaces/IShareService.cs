using BLL.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
   public interface IShareService
    {
        public string ValidPhoneNumber(string phoneNumber);
        List<DoctorDto> GetDoctors();
        List<ClincDto>GetClincs();
        List<PatientDto> GetPatients();
        List<MedicalServiceDto> GetMSByClinic(int clinicId);
        List<DoctorDto> GetDoctorsByClinic(int clinicId);
    }
}
