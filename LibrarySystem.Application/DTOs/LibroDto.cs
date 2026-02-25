using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.DTOs
{
    public class LibroDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título del libro es obligatorio")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre del autor es obligatorio")] 
        public string Autor {  get; set; } = string.Empty;

        [Required(ErrorMessage = "El stock del libro es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage ="El stock no puede ser negativo")]
        public int? Stock { get; set; }

        [Required(ErrorMessage ="Seleccione una categoría")]
        public int? CategoriaId { get; set; }
        public string NombreCategoria { get; set; } = string.Empty;
    }
}
