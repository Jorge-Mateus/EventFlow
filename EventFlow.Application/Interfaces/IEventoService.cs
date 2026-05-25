using EventFlow.Application.DTOs.Evento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Interfaces
{
    public interface IEventoService
    {
        Task CriarAsync(CriarEventoDto dto);
        Task<IEnumerable<EventoDto>> ObterTodosAsync();
        Task<EventoDto?> ObterPorIdAsync(Guid id);
        Task<EventoDetalheDto?> ObterDetalheAsync(Guid id);
        Task AtualizarAsync(AtualizarEventoDto dto);
        Task ExcluirAsync(Guid id);
    }
}
