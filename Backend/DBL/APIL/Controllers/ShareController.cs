using BLL.Interfaces;
using DBL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShareController : ControllerBase
    {
        private readonly IShareService _shareService;
        public ShareController(IShareService shareService)
        {
            _shareService = shareService;
        }

        [HttpGet("Docterlist")]
        public IActionResult GetDoctors()
        {
            var query = _shareService.GetDoctors();
            return Ok(query);

        }
        [HttpGet("Patientslist")]
        public IActionResult GetPatients()
        {
            var query = _shareService.GetPatients();
            return Ok(query);

        }
        [HttpGet("Clinclist")]
        public IActionResult GetClincs()
        {
            var query = _shareService.GetClincs();
            return Ok(query);

        }
        [HttpGet("GetMSByClinic")]
        public IActionResult GetMSByClinic(int clinicId)
        {
            try
            {
                var result = _shareService.GetMSByClinic(clinicId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("GetDoctorsByClinic")]
        public IActionResult GetDoctorsByClinic(int clinicId)
        {
            try
            {
                var result = _shareService.GetDoctorsByClinic(clinicId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
