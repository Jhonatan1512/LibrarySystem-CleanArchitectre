using LibrarySystem.Application.DTOs;
using LibrarySystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {

        private readonly IPrestamoService _prestamoService;

        public PrestamoController(IPrestamoService prestamoService)
        {
            _prestamoService = prestamoService;
        }

        //POST :api/prestamo 
        [HttpPost]
        public async Task<IActionResult> CrearPrestamo([FromBody] PrestamoDto dto)
        {
            var resultado = await _prestamoService.ProcesarPrestamo(dto);

            if (resultado != "Ok")
            {
                return BadRequest(new { mensaje = resultado });

            }

            return StatusCode(201, new { mensaje = "Prestamo registrado" });
        }

        //GET :api/prestamo
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var prestamos = await _prestamoService.ObtenerTodosAsync();
            return Ok(prestamos);
        }

        //GET api/prestamo/id
        [HttpGet("{dni}")]
        public async Task<ActionResult> ObtenerPorDni(string dni)
        {
            var prestamo = await _prestamoService.ObtenerPorDniAsync(dni);

            if (!prestamo.Any()) return NotFound(new { mensaje = $"No se encontro prestamos para el usuario con DNI: {dni}" });
            return Ok(prestamo);
        }

        //GET :api/id
        [HttpGet("id/{id}")]
        public async Task<ActionResult<PrestamoDto>> ObtenerPorId(int id)
        {
            var prestamo = await _prestamoService.ObtenerPorIdAsync(id);

            if (prestamo == null) return NotFound(new { mensaje = "Prestamo no encontrado" });

            var presatmoDto = new PrestamoDto
            {
                Id = prestamo.Id,                
            };
            return Ok(presatmoDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Entregar(int id)
        {
            var prestamoExiste = await _prestamoService.ObtenerPorIdAsync(id);
            if (prestamoExiste == null) return NotFound("El prestamo no existe");

            //Al asignar la fecha actual, el estado automáticamente pasará a "Entregado"
            await _prestamoService.RegistrarEntregaAsync(id);
            //prestamoExiste.FechaDevolucionReal = DateTime.UtcNow;

            return Ok(new { 
                mensaje = "Entrega registrada con éxito", 
                fecha = prestamoExiste.FechaDevolucionReal 
            });
        }
    }
}
