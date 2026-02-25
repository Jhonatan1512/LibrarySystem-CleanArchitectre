using LibrarySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Interfaces
{
    public interface IPrestamoRepository
    {
        Task<Prestamo> RegistrarPrestamoAsync(Prestamo prestamo, List<Libro> libros);
        Task<IEnumerable<Prestamo>> ObtenerTodosAsync();
        Task<IEnumerable<Prestamo>> ObtenerPorDniAsync(string dni);
    }
}
