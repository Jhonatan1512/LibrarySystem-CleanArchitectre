using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.DTOs
{
    public class UsuarioDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre del usuario es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage ="El DNI del usuario es obligatrio")]
        [StringLength(8, MinimumLength = 8, ErrorMessage ="El Dni debe tener 8 caracteres")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage ="El Dni  solo debe contener valores numéricos")]
        public string Dni { get; set; } = string.Empty;
    }
}
