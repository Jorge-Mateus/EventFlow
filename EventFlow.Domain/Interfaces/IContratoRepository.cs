using EventFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Domain.Interfaces
{
    public interface IContratoRepository
    {
        Task AdicionarAsync(Contrato contrato);
        Task AtualizarAsync(Contrato contrato);
        Task<Contrato?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Contrato>> ObterPorEventoAsync(Guid eventoId);
        Task RemoverAsync(Contrato contrato);
        Task SalvarAlteracoesAsync();
    }
}
