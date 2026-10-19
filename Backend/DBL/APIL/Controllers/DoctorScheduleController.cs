using BLL.Interfaces;
using BLL.Service;
using BLL.Service.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorScheduleController : ControllerBase
    {
        private readonly IDoctorScheduleService _doctorScheduleService;

        public DoctorScheduleController(IDoctorScheduleService doctorScheduleService)
        {
            _doctorScheduleService = doctorScheduleService;
        }

        [HttpPost]
        public IActionResult Create(CreateDoctorScheduleDto dto)
        {
            try
            {
                var result = _doctorScheduleService.Create(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _doctorScheduleService.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _doctorScheduleService.Delete(id);

            if (!result)
                return NotFound();

            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetFilter(int? DoctorId, DayOfWeek? DayOfWeek)
        {
            var result = _doctorScheduleService.GetFilter(DoctorId, DayOfWeek);

            return Ok(result);
        }
    }
}
