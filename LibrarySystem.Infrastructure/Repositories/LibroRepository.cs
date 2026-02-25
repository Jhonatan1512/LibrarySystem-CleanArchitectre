using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Infrastructure.Data;

namespace LibrarySystem.Infrastructure.Repositories
{ 
    public class LibroRepository : ILibroRepository
    {
        private readonly AplicationDbContext _context;

        public LibroRepository(AplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Libro>> ObtenerTodosAsync()
        {
            return await _context.Libros.Include(l => l.Categoria).ToListAsync();
        }
         
        public async Task ActualizarAsync(Libro libro)
        {
            _context.Libros.Update(libro);
            await _context.SaveChangesAsync();
        }

        public async Task AgregarAsync(Libro libro)
        {
            await _context.Libros.AddAsync(libro);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro != null) 
            {
                _context.Libros.Remove(libro);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Libro?> ObtenerPorIdAsync(int id)
        {
            return await _context.Libros.Include(l => l.Categoria).FirstOrDefaultAsync(l => l.Id == id);
        }
    }
}
