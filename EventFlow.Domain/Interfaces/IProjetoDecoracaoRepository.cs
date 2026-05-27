using EventFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Domain.Interfaces
{
    public interface IProjetoDecoracaoRepository
    {
        Task AdicionarAsync(ProjetoDecoracao projeto);
        Task<ProjetoDecoracao?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<ProjetoDecoracao>> ObterTodosAsync();
        Task SalvarAlteracoesAsync();
        Task AtualizarAsync(ProjetoDecoracao projeto);
        Task RemoverAsync(ProjetoDecoracao projeto);
        Task RemoverArquivoAsync(Guid projetoId);
        Task AdicionarArquivoAsync(ProjetoArquivo arquivo);
    }
}
