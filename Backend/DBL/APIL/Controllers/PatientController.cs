using BLL.Interfaces;
using BLL.Service.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public IActionResult GetAll(
            string? fullName,
            string? phoneNumber,
            string? nationalId)
        {
            var patients = _patientService.GetAll(
                fullName,
                phoneNumber,
                nationalId);

            return Ok(patients);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var patient = _patientService.GetById(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        [HttpPost]
        public IActionResult Create(CreatePatientDto dto)
        {
            try
            {
                var result = _patientService.Create(dto);

                return StatusCode(201, result);
            }
            catch (Exception ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdatePatientDto dto)
        {
            try
            {
                var result = _patientService.Update(id, dto);

                if (!result)
                    return NotFound(new
                    {
                        message = "Patient not found."
                    });

                return Ok(new
                {
                    message = "Patient updated successfully."
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
            var result = _patientService.Delete(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}

