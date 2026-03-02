using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Infrastructure.Repositories
{
    public class PrestamoRepository : IPrestamoRepository
    {
        public readonly AplicationDbContext _context;

        public PrestamoRepository(AplicationDbContext context)
        {
            _context = context;
        }
         
        public async Task<Prestamo> RegistrarPrestamoAsync(Prestamo prestamo, List<Libro> libros)
        {
            await _context.Prestamos.AddAsync(prestamo);

            foreach (var libro in libros)  
            {
                if (libro.Stock <= 0) throw new Exception($"No hay stock para el libro : {libro.Titulo}");

                libro.Stock -= 1;
                _context.Libros.Update(libro);
            }

            await _context.SaveChangesAsync();

            return prestamo;
        }

        public async Task<IEnumerable<Prestamo>> ObtenerTodosAsync()
        {
            return await _context.Prestamos.Include(p => p.Usuario).Include(p => p.Detalles).ThenInclude(d => d.Libro).ToListAsync();
        }

        public async Task<IEnumerable<Prestamo>> ObtenerPorDniAsync(string dni)
        {
            return await _context.Prestamos
                .Include(p => p.Usuario)
                .Include(p => p.Detalles)
                .ThenInclude(d => d.Libro)
                .Where(p => p.Usuario != null && p.Usuario.Dni == dni).ToListAsync();
        }

        public async Task RegistrarEntregaAsync(int id)
        {
            var prestamo = await _context.Prestamos.Include(p => p.Detalles).FirstOrDefaultAsync(d => d.Id == id);
            if (prestamo != null && !prestamo.FechaDevolucionReal.HasValue)
            {
                prestamo.FechaDevolucionReal = DateTime.UtcNow;

                foreach(var detalle in prestamo.Detalles)
                {
                    var libro = await _context.Libros.FindAsync(detalle.LibroId);

                    if (libro != null) 
                    {
                        libro.Stock += 1;
                    }
                }
                
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Prestamo?> ObtenerPorId(int id)
        {
            return await _context.Prestamos.Include(p => p.Detalles).FirstOrDefaultAsync(p => p.Id ==  id);
        }
    }
}
