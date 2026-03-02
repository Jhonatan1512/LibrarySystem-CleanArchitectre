using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.DTOs
{
    public class PrestamoDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }  
        public List<int> LibrosIds { get; set; } = new List<int>(); //ids de los libros a prestar
        public int DiasPrestamo {  get; set; }

    }
}
