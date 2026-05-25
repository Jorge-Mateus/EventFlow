using EventFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task AdicionarAsync(Cliente cliente);
        Task AtualizarAsync(Cliente cliente);
        Task<Cliente?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Cliente>> ObterTodosAsync();
        Task SalvarAlteracoesAsync();
        Task RemoverAsync(Cliente cliente);
    }
}
