using LibrarySystem.Application.DTOs;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Services
{
    public class PrestamoService : IPrestamoService
    {
        private readonly IPrestamoRepository _prestamoServiceRep;
        private readonly ILibroRepository _libroRepository;


        public PrestamoService(IPrestamoRepository prestamoRepo, ILibroRepository libroRepository)
        {
            _prestamoServiceRep = prestamoRepo;
            _libroRepository = libroRepository;

        }

        public async Task<string> ProcesarPrestamo(PrestamoDto dto)
        {
            if (dto.LibrosIds == null || !dto.LibrosIds.Any()) return "Debe seleccionar al menos un libro";

            var lisbrosPrestar = new List<Libro>();

            //validamos cada libro
            foreach (var id in dto.LibrosIds)
            {
                var libros = await _libroRepository.ObtenerPorIdAsync(id);
                if (libros == null) return $"El libro {libros.Titulo} no existe.";
                if (libros.Stock <= 0) return $"El libro {libros.Titulo} no tiene stock";

                lisbrosPrestar.Add(libros);
            }

            var nuevoPrestamo = new Prestamo
            {
                UsuarioId = dto.UsuarioId,
                FechaPrestamo = DateTime.UtcNow,
                FechaDevolucion = DateTime.Now.AddDays(dto.DiasPrestamo),

                Detalles = lisbrosPrestar.Select(l => new DetallePrestamo
                {
                    LibroId = l.Id,
                }).ToList(),
            };

            await _prestamoServiceRep.RegistrarPrestamoAsync(nuevoPrestamo, lisbrosPrestar);
            return "Ok";
        }

        public async Task<IEnumerable<GetPrestamoDto>> ObtenerTodosAsync()
        {
            var prestamos = await _prestamoServiceRep.ObtenerTodosAsync();

            return prestamos.Select(p => new GetPrestamoDto 
            {
                Id = p.Id,
                NombreUsuario = p.Usuario?.Nombre ?? "Dsconocido",
                FechaPrestamo = p.FechaPrestamo,
                FechaDevolucion = p.FechaDevolucion,
                LibrosPrestados = p.Detalles.Select(d => d.Libro?.Titulo ?? "Sin titulo").ToList(),
            });
        }

        public async Task<IEnumerable<GetPrestamoDto>> ObtenerPorDniAsync(string dni)
        {
            var prestamo = await _prestamoServiceRep.ObtenerPorDniAsync(dni);

            return prestamo.Select(p => new GetPrestamoDto
            {
                Id = p.Id,
                NombreUsuario = p.Usuario?.Nombre ?? "Desconocido",
                FechaPrestamo = p.FechaPrestamo,
                FechaDevolucion = p.FechaDevolucion,
                LibrosPrestados = p.Detalles.Select(d => d.Libro?.Titulo ?? "Sin título").ToList()
            });
        }

    }
}
