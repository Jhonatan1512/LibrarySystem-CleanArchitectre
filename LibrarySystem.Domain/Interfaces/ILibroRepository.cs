using LibrarySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Interfaces
{
    public interface ILibroRepository
    {
        Task<IEnumerable<Libro>> ObtenerTodosAsync();
        Task<Libro?> ObtenerPorIdAsync(int id); 
        Task AgregarAsync(Libro libro);
        Task ActualizarAsync(Libro libro);
        Task EliminarAsync(int id);
        Task<IEnumerable<Libro>> ObtenerPorTituloAsync(string titulo);

    }
}
