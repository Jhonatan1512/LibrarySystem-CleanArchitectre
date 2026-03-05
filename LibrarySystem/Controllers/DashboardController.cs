using Microsoft.AspNetCore.Mvc;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Application.DTOs;

namespace LibrarySystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashbradRepository _repository;

        public DashboardController(IDashbradRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("resumen")]
        public async Task<ActionResult<DashboarDto>> getResumen()
        {
            var data = await _repository.GetResumenAsync();
            return Ok(data);
        }

    }
}
