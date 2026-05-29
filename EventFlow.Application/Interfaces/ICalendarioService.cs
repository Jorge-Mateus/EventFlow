using EventFlow.Application.DTOs.Calendario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Interfaces
{
    public interface ICalendarioService
    {
        Task<IEnumerable<CalendarioEventoDto>> ObterAsync();
    }
}
