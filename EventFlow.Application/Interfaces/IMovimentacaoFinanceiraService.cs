using EventFlow.Application.DTOs.MovimentacaoFinanceira;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Interfaces
{
    public interface IMovimentacaoFinanceiraService
    {
        Task CriarAsync(CriarMovimentacaoFinanceiraDto dto);
        Task AtualizarAsync(AtualizarMovimentacaoFinanceiraDto dto);
        Task<IEnumerable<MovimentacaoFinanceiraDto>> ObterPorEventoAsync(Guid eventoId);
        Task<ResumoFinanceiroDto> ObterResumoAsync(Guid eventoId);
        Task ExcluirAsync(Guid id);
    }
}
