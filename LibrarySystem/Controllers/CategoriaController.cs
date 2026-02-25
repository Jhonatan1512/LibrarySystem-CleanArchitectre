using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Application.DTOs;

namespace LibrarySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository) 
        {
            _categoriaRepository = categoriaRepository;
        }

        //GET :api/categoria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> ObtenerTodos()
        {
            var categorias = await _categoriaRepository.ObtenerTodosAsync();

            var categoriasDto = categorias.Select(c => new CategoriaDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
            });
            return Ok(categoriasDto);
        }

        //GET :api/categoria/id
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Categoria>>> ObtenerId(int id)
        {
            var categoria = await _categoriaRepository.ObtenerPorIdAsync(id);

            if (categoria == null) return NotFound("Categoría no encontrada");

            var categoriaDto = new CategoriaDto
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
            };
            return Ok(categoriaDto);
        }

        //POST :api/categoria
        [HttpPost]
        public async Task<ActionResult> AgregarCategoria([FromBody] CategoriaDto categoriaDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var categoriaNuevo = new Categoria
            {
                Id = categoriaDto.Id,
                Nombre = categoriaDto.Nombre,
            };
            await _categoriaRepository.AgregarAsync(categoriaNuevo);
            return Ok(categoriaNuevo);
        }

        //PUT :api/categoria/id
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarCategoria(int id, [FromBody] CategoriaDto categoriaDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var categoriaExistente = await _categoriaRepository.ObtenerPorIdAsync(id);
            if(categoriaExistente == null) return NotFound("Categoría no encontrada");

            categoriaExistente.Nombre = categoriaDto.Nombre;

            await _categoriaRepository.ActualizarAsync(categoriaExistente);
            return Ok(new { mensaje = "Categoría actualizada"});
        }

        //DELETE :api/categoria/id
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarCategoria(int id)
        {
            var categoriaExiste = await _categoriaRepository.ObtenerPorIdAsync(id);
            if (categoriaExiste == null) return NotFound("Categoría no encontrada");

            await _categoriaRepository.EliminarAsync(id);
            return Ok(new { mensaje = "Categoría eliminada" });
        }
    }
}
