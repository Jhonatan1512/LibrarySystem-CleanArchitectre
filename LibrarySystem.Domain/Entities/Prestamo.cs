using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Entities
{
    public class Prestamo
    {
        public int Id { get; set; }
        public DateTime FechaPrestamo { get; set; } = DateTime.UtcNow;
        public DateTime FechaDevolucion { get; set; }
        public DateTime? FechaDevolucionReal { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; } 

        public ICollection<DetallePrestamo> Detalles { get; set; } = new List<DetallePrestamo>(); 
    }
}
