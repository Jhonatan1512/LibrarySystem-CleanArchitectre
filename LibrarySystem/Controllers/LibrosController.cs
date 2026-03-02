using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Application.DTOs;

namespace LibrarySystem.Api.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ILibroRepository _libroRepository;

        private readonly ICategoriaRepository _categoriaRepository;

        //Inyectamos la interfaz, no la implemetación directa
        public LibrosController(ILibroRepository libroRepository, ICategoriaRepository categoriaRepository)
        {
            _libroRepository = libroRepository;
            _categoriaRepository = categoriaRepository;
        }

        //GET :api/libros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroDto>>> ObtenerTodos()
        {
            var libros = await _libroRepository.ObtenerTodosAsync();

            //Mapeamos (convertimos) la entidad al DTO para no exponer datos extra

            var librosDto = libros.Select(l => new LibroDto
            {
                Id = l.Id,
                Titulo = l.Titulo,
                Autor = l.Autor,
                Stock = l.Stock,
                CategoriaId = l.CategoriaId,
                NombreCategoria = l.Categoria?.Nombre ?? "Sin Categoría"
            });
            return Ok(librosDto);
        }

        //GET :api/libros/id
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<LibroDto>>> ObtenerPorId(int id)
        {
            var libro = await _libroRepository.ObtenerPorIdAsync(id);

            if (libro == null) return NotFound("El libro no existe en la Base de Datos");

            var libroDto = new LibroDto
            {
                Id = libro.Id,
                Titulo = libro.Titulo,
                Autor = libro.Autor,
                Stock = libro.Stock,
                CategoriaId = libro.CategoriaId,
                NombreCategoria = libro.Categoria?.Nombre ?? "Sin Categoría"
            };
            return Ok(libroDto);
        }

        //GET :api/libros/titulo
        [HttpGet("titulo/{titulo}")]
        public async Task<ActionResult<IEnumerable<LibroDto>>> ObtenerPorTitulo(string titulo)
        {
            var libro = await _libroRepository.ObtenerPorTituloAsync(titulo);

            if (libro == null || !libro.Any()) return Ok(new List<LibroDto>());

            var libroDto = libro.Select(l => new LibroDto
            {
                Id = l.Id,
                Titulo = l.Titulo,
                Autor = l.Autor,
                Stock = l.Stock,
                CategoriaId = l.CategoriaId,
                NombreCategoria = l.Categoria?.Nombre ?? "Sin Categoría"
            });
            return Ok(libroDto);
        }

        //POST :api/libros
        [HttpPost]
        public async Task<ActionResult> CrearLibro([FromBody] LibroDto libroDto) 
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var categoriaExiste = await _categoriaRepository.ExisteAsync(libroDto.CategoriaId.Value);

            if (!categoriaExiste)
            {
                return NotFound(new { message = "Categoría no encontrada" });
            }

            var nuevoLibro = new Libro
            {
                Titulo = libroDto.Titulo,
                Autor = libroDto.Autor,
                Stock = libroDto.Stock.Value,
                CategoriaId = libroDto.CategoriaId.Value,
            };
            await _libroRepository.AgregarAsync(nuevoLibro);

            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoLibro.Id }, libroDto);
        }

        //PUT :api/libros/id
        [HttpPut("{id}")]
        public async Task<ActionResult> AcctualizarLibro(int id, [FromBody] LibroDto libroDto)
        { 
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var libroExistente = await _libroRepository.ObtenerPorIdAsync(id);
            if (libroExistente == null) return NotFound("Libro no encotrado");

            libroExistente.Titulo = libroDto.Titulo;
            libroExistente.Autor = libroDto.Autor;
            libroExistente.Stock = libroDto.Stock.Value;
            libroExistente.CategoriaId = libroDto.CategoriaId.Value;

            await _libroRepository.ActualizarAsync(libroExistente);

            return Ok(new {mensaje = "Libro actualizado"});
        }

        //DELETE : api/libros/id
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarLibro(int id)
        {
            var libroExiste = await _libroRepository.ObtenerPorIdAsync(id);
            if (libroExiste == null) return NotFound("Libro no encontrado");

            await _libroRepository.EliminarAsync(id);
            return Ok(new {mensaje = "Libro eliminado"});
        }
    }
}
