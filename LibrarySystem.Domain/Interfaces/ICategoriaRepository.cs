using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> ObtenerTodosAsync();
        Task<Categoria?> ObtenerPorIdAsync(int id);
        Task AgregarAsync(Categoria categoria);
        Task ActualizarAsync(Categoria categoria); 
        Task EliminarAsync(int id);
        Task<bool> ExisteAsync(int id);

    }
}
