using BLL.Interfaces;
using BLL.Interfaces.BLL.Interfaces;
using BLL.Service;
using BLL.Service.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClincController : ControllerBase
    {
        private readonly IClincService _clincService;

        public ClincController(IClincService clincService)
        {
            _clincService = clincService;
        }
        [HttpGet]
        public IActionResult GetAll(string? ClincName, string? ClincCode, bool? IsActive)
        {
            var result = _clincService.GetAll(ClincName,ClincCode,IsActive);

            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _clincService.GetById(id);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Clinc  not found"
                });
            }

            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create(CreateClincDto dto)
        {
            try
            {
                var result = _clincService.Create(dto);

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
        public IActionResult Update(int id,UpdateClincDto dto)
        {
            try
            {
                var result = _clincService.Update(id, dto);

                if (!result)
                    return NotFound(new
                    {
                        message = "clinic not found."
                    });

                return Ok(new
                {
                    message = "clinic updated successfully."
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
                var result = _clincService.Delete(id);

                if (!result)
                    return NotFound(new
                    {
                        message = "clinic not found."
                    });

                return Ok(new
                {
                    message = "clinic updated successfully."
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

    }
}
