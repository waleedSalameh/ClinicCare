using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        
        [HttpGet("appointments-by-clinic")]
        public IActionResult GetAppointmentsByClinic()
        {
            return Ok(_dashboardService.GetClincByAppointmets());
        }

       
        [HttpGet("summary")]
        public IActionResult GetSummary()
        {
            return Ok(_dashboardService.GetSummary());
        }

       
        [HttpGet("today-appointments")]
        public IActionResult GetTodayAppointments()
        {
            return Ok(_dashboardService.GetTodayAppointments());
        }
    }
}
    

