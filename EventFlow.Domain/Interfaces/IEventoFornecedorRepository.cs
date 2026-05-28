using EventFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Domain.Interfaces
{
    public interface IEventoFornecedorRepository
    {
        Task AdicionarAsync(EventoFornecedor fornecedor);
        Task<IEnumerable<EventoFornecedor>>ObterPorEventoAsync(Guid eventoId);
        Task RemoverAsync(EventoFornecedor fornecedor);
        Task SalvarAlteracoesAsync();
    }
}
