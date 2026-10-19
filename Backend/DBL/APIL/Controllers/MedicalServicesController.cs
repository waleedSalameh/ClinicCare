using BLL.Interfaces;
using BLL.Service.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalServicesController : ControllerBase
    {
        private readonly IMedicalServiceService _service;

        public MedicalServicesController(IMedicalServiceService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll(string? serviceName,string? serviceCode,int? isActive)
        {
            var result = _service.GatAll(
                serviceName,
                serviceCode,
                isActive);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Medical service not found"
                });
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(CreateMedicalServiceDto dto)
        {
            try
            {
                var result = _service.Create(dto);

                return StatusCode(
                    StatusCodes.Status201Created,
                    result);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        message = ex.Message
                    });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(
            int id,
            UpdateMedicalServiceDto dto)
        {
            var result = _service.Update(id, dto);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Medical service not found"
                });
            }

            return Ok(new
            {
                message = "Medical service updated successfully"
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Medical service not found"
                });
            }

            return Ok(new
            {
                message = "Medical service deleted successfully"
            });
        }
    }
}

