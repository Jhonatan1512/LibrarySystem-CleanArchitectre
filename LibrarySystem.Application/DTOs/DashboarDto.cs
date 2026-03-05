using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.DTOs
{
    public class DashboarDto
    {
        public int totalLibros {  get; set; }
        public int prestamosActivos { get; set; }
        public int lectoresRegistrados { get; set; }
        public List<CategoriaStatDto> categoriasMasPrestadas { get; set; }
    }

    public class CategoriaStatDto
    {
        public string Nombre { get; set; }
        public int Total {  get; set; }
        public double Porcentaje { get; set; }
    }
}
