using BLL.Interfaces;
using BLL.Service.Dto;
using BLL.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtyController : ControllerBase
    {
        private readonly ISpecialtyService _specialtyService;
        public SpecialtyController(ISpecialtyService specialtyService)
        {
            _specialtyService = specialtyService;
        }



        [HttpPost]
        public IActionResult Create(CreateSpecialtyDto dto)
        {
            try
            {
                var result = _specialtyService.Create(dto);

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

        [HttpGet]
        public IActionResult GetAll(string? Name, string? Code, bool? IsActive)
        {
            var result = _specialtyService.GetAll(Name, Code, IsActive);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _specialtyService.GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("active")]
        public IActionResult GetActive()
        {
            var result = _specialtyService.GetActive();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateSpecialtyDto dto)
        {
            try
            {
                var result = _specialtyService.Update(id, dto);

                if (!result)
                    return NotFound(new
                    {
                        message = "specialty not found."
                    });

                return Ok(new
                {
                    message = "specialty updated successfully."
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
                var result = _specialtyService.Delete(id);

                if (!result)
                    return NotFound(new
                    {
                        message = "specialty not found."
                    });

                return Ok(new
                {
                    message = "specialty updated successfully."
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

