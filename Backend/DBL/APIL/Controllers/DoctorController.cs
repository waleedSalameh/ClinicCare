using BLL.Interfaces;
using BLL.Interfaces.BLL.Interfaces;
using BLL.Service;
using BLL.Service.Dto;
using Microsoft.AspNetCore.Mvc;

namespace APIL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public IActionResult GetAll(string? fullName,string? phoneNumber,int? specialtyId)
        {
            var doctors = _doctorService.GetAll(
                fullName,
                phoneNumber,
                specialtyId);

            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var doctor = _doctorService.GetById(id);

            if (doctor == null)
                return NotFound();

            return Ok(doctor);
        }

        [HttpPost]
        public IActionResult Create(CreateDoctorDto dto)
        {
            var newDoctor = _doctorService.Create(dto);

            return Ok(newDoctor);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,UpdateDoctorDto dto)
        {
            try
            {
                var result = _doctorService.Update(id, dto);

                if (!result)
                    return NotFound(new
                    {
                        message = "doctor not found."
                    });

                return Ok(new
                {
                    message = "doctor updated successfully."
                });
            }
            catch (Exception ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _doctorService.Delete(id);

                if (!result)
                    return NotFound(new
                    {
                        message = "doctor not found."
                    });

                return Ok(new
                {
                    message = "doctor updated successfully."
                });
            }
            catch (Exception ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
        }
        [HttpGet("specialties")]
        public IActionResult GetSpecialties()
        {
            var specialties =
                _doctorService.GetSpecialties();

            return Ok(specialties);
        }
    }
}