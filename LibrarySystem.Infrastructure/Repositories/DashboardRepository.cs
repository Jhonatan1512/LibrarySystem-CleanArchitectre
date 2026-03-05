using LibrarySystem.Application.DTOs;
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
    public class DashboardRepository : IDashbradRepository
    {
        private readonly AplicationDbContext _context;

        public DashboardRepository(AplicationDbContext context)
        {
            _context = context;
        }
        public async Task<DashboarDto> GetResumenAsync()
        {
            var totalLibros = await _context.Libros.CountAsync();

            var prestamosActivos = await _context.Prestamos.CountAsync(p => p.FechaDevolucionReal == null);
            var lectores = await _context.Usuarios.CountAsync();

            var statsCategorias = await _context.Prestamos
                .Where(p => p.FechaDevolucionReal == null)
                .SelectMany(p => p.Detalles)
                .GroupBy(l => l.Libro.Categoria.Nombre)
                .Select(g => new CategoriaStatDto
                {
                    Nombre = g.Key,
                    Total = g.Count(),
                })
                .OrderByDescending(x => x.Total)
                .Take(7)
                .ToListAsync();

            int maxPrestamos = statsCategorias.Any() ? statsCategorias.Max(x => x.Total) : 1;
            statsCategorias.ForEach(x => x.Porcentaje = (double)x.Total / maxPrestamos * 100);

            return new DashboarDto
            {
                totalLibros = totalLibros,
                prestamosActivos = prestamosActivos,
                lectoresRegistrados = lectores,
                categoriasMasPrestadas = statsCategorias,
            };

        }
    }
}
