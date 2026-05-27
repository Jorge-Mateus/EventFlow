using EventFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Domain.Interfaces
{
    public interface IColaboradorRepository
    {
        Task AdicionarAsync(Colaborador colaborador);
        Task AtualizarAsync(Colaborador colaborador);
        Task<Colaborador?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Colaborador>> ObterTodosAsync();
        Task RemoverAsync(Colaborador colaborador);
        Task SalvarAlteracoesAsync();
    }
}
