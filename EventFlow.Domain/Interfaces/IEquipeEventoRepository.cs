using EventFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Domain.Interfaces
{
    public interface IEquipeEventoRepository
    {
        Task AdicionarAsync(EquipeEvento equipeEvento);
        Task AtualizarAsync(EquipeEvento equipeEvento);
        Task<EquipeEvento?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<EquipeEvento>> ObterTodosAsync();
        Task<IEnumerable<EquipeEvento>> ObterPorEventoAsync(Guid eventoId);
        Task RemoverAsync(EquipeEvento equipeEvento);
        Task SalvarAlteracoesAsync();
    }
}
