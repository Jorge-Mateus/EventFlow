using EventFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Domain.Interfaces
{
    public interface IFuncaoRepository
    {
        Task AdicionarAsync(Funcao funcao);
        Task AtualizarAsync(Funcao funcao);
        Task<Funcao?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Funcao>> ObterTodosAsync();
        Task RemoverAsync(Funcao funcao);
        Task SalvarAlteracoesAsync();
    }
}
