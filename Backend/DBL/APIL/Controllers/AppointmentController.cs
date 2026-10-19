using BLL.Interfaces;
using BLL.Service;
using BLL.Service.Dto;
using DBL.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        public IActionResult Create(CreateAppointmentDto dto)
        {
            try
            {
                var result = _appointmentService.Create(dto);

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

        [HttpGet]
        public IActionResult GetAll( DateTime? FromDate,DateTime? ToDate, int? DoctorId, int? PatientId,int? ClincId,AppointmentStatus? Status)
        {
            var result = _appointmentService.GetAll(FromDate, ToDate, DoctorId,  PatientId, ClincId, Status);

            return Ok(result);
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _appointmentService.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateAppointmentDto dto)
        {
            try
            {
                var result = _appointmentService.Update(id, dto);

                if (!result)
                    return NotFound();

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

        [HttpPatch("{id}")]
        public IActionResult Change(int id, ChangeAppointmentStatusDto dto)
        {
            try
            {
                var result = _appointmentService.Change(id, dto);

                if (!result)
                    return NotFound();

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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _appointmentService.Delete(id);

                if (!result)
                    return NotFound();

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
        [HttpGet("BookingAppointments")]
        public IActionResult BookingAppointments( int clincId, int MSId,int DoctorId, DateTime AppointmentDate)
        {
            try
            {
                var result = _appointmentService.BookingAppointments(
                    clincId,
                    MSId,
                    DoctorId,
                    AppointmentDate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
