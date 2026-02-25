using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Entities
{
    public class DetallePrestamo
    {
        public int Id { get; set; }

        public int PrestamoId { get; set; }
        public Prestamo? Prestamo { get; set; }

        public int LibroId { get; set; }
        public Libro? Libro { get; set; } 

    }
}
