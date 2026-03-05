using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Application.DTOs;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.Interfaces
{
    public interface IDashbradRepository
    {
        Task<DashboarDto> GetResumenAsync();
    }
}
