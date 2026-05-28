using EventFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Domain.Interfaces
{
    public interface IMovimentacaoFinanceiraRepository
    {
        Task AdicionarAsync(MovimentacaoFinanceira movimentacao);
        Task AtualizarAsync(MovimentacaoFinanceira movimentacao);
        Task<MovimentacaoFinanceira?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<MovimentacaoFinanceira>> ObterPorEventoAsync(Guid eventoId);
        Task RemoverAsync(MovimentacaoFinanceira movimentacao);
        Task<IEnumerable<MovimentacaoFinanceira>> ObterTodosAsync();
        Task SalvarAlteracoesAsync();
    }
}
