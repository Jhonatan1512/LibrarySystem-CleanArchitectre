using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre {  get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
    }
}
