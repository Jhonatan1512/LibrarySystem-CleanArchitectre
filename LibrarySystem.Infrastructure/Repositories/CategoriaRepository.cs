using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Infrastructure.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AplicationDbContext _context;

        public CategoriaRepository(AplicationDbContext context)
        {
            _context = context;
        }
         
        public async Task ActualizarAsync(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task AgregarAsync(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria != null) 
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Categorias.AnyAsync(c => c.Id == id);
        }

        public async Task<Categoria?> ObtenerPorIdAsync(int id)
        {
            return await _context.Categorias.Include(c => c.Libros).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Categoria>> ObtenerTodosAsync()
        {
            return await _context.Categorias.Include(c => c.Libros).ToListAsync();
        }
    }
}
