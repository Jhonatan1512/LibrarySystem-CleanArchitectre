
using LibrarySystem.Application.DTOs;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace LibrarySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository) 
        {
            _usuarioRepository = usuarioRepository;
        }

        //GET :api/usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> ObtenerTodos()
        {
            var usuarios = await _usuarioRepository.ObtenerTodosAsync();
            var usuariosDto = usuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Dni = u.Dni,
            });
            return Ok(usuariosDto);
        }

        //GET :api/usuario/id
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Usuario>>> ObtenerPorId(int id)
        {
            var usuario = await _usuarioRepository.ObtenerPorIdAsync(id);

            if (usuario == null) return NotFound("Usuario no encontrado");

            var usuarioDto = new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Dni = usuario.Dni,
            };

            return Ok(usuarioDto);
        }

        //POST :api/usuario
        [HttpPost]
        public async Task<ActionResult> AgregarUsuario([FromBody] UsuarioDto usuarioDto) 
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var usuarionuevo = new Usuario
            {
                Nombre = usuarioDto.Nombre,
                Dni = usuarioDto.Dni,
            };

            await _usuarioRepository.CrearUsuarioAsync(usuarionuevo);

            return StatusCode(201, new {mensaje = "Usuario creado correctamente"});
        }

        //PUT :api/usuario/id
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] UsuarioDto usuarioDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var usuarioExiste = await _usuarioRepository.ObtenerPorIdAsync(id);
            if(usuarioExiste == null) return NotFound("Usuario no encontrado");

            usuarioExiste.Nombre = usuarioDto.Nombre;
            usuarioExiste.Dni = usuarioDto.Dni;

            await _usuarioRepository.ActualizarUsuarioAsync(usuarioExiste);

            return StatusCode(201, new { mensaje = "Usuario Actualizado" });
        }

        //DELETE :api/usuario/id
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarUsuario(int id)
        {
            var usuarioExiste = await _usuarioRepository.ObtenerPorIdAsync(id);
            if(usuarioExiste == null) return NotFound("Usuario no encontrado");

            await _usuarioRepository.EliminarUsuario(id);
            return StatusCode(201, new { mensaje = "Usuario eliminado" });
        }
    }
}
