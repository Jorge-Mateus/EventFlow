using EventFlow.Application.DTOs.EquipeEvento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Interfaces
{
    public interface IEquipeEventoService
    {
        Task CriarAsync(CriarEquipeEventoDto dto);
        Task AtualizarAsync(AtualizarEquipeEventoDto dto);
        Task<EquipeEventoDto?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<EquipeEventoDto>> ObterTodosAsync();
        Task<IEnumerable<EquipeEventoDto>> ObterPorEventoAsync(Guid eventoId);
        Task RemoverAsync(Guid id);
    }
}
