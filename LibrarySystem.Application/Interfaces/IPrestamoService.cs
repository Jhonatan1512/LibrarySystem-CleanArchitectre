using LibrarySystem.Application.DTOs;
using LibrarySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Interfaces
{
    public interface IPrestamoService
    {
        Task<string> ProcesarPrestamo(PrestamoDto dto);
        Task<IEnumerable<GetPrestamoDto>> ObtenerTodosAsync();
        Task<IEnumerable<GetPrestamoDto>> ObtenerPorDniAsync(string dni);
        Task RegistrarEntregaAsync(int id);
        Task<Prestamo?> ObtenerPorIdAsync(int id);
    }
}
 