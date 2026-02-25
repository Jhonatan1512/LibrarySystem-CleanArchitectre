using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.DTOs
{
    public class GetPrestamoDto
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaDevolucion { get; set; }
        public List<string> LibrosPrestados { get; set; } = new List<string>();
    }
}
