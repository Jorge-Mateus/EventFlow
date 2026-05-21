using EventFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Domain.Interfaces
{
    public interface IPropostaRepository
    {
        Task AdicionarAsync(Proposta proposta);
        Task AtualizarAsync(Proposta proposta);
        Task<Proposta?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Proposta>> ObterTodosAsync();
        Task SalvarAlteracoesAsync();
    }
}
