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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AplicationDbContext _context;

        public UsuarioRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> ObtenerTodosAsync()
        {
            return await _context.Usuarios.ToArrayAsync();
        }
        public async Task<Usuario?> ObtenerPorIdAsync(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);  
        }

        public async Task CrearUsuarioAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarUsuarioAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await  _context.SaveChangesAsync();
        }

        public async Task EliminarUsuario(int id)
        {
            var usuarioExiste = await _context.Usuarios.FindAsync(id);
            if(usuarioExiste != null)
            {
                _context.Usuarios.Remove(usuarioExiste);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Usuario?> ObtenerPorDniAsync(string dni)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Dni == dni);
        }
    }
}
