using EventFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Domain.Interfaces
{
    public interface IVisitaTecnicaRepository
    {
        Task AdicionarAsync(VisitaTecnica visita);
        Task AtualizarAsync(VisitaTecnica visita);
        Task<VisitaTecnica?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<VisitaTecnica>> ObterTodosAsync();
        Task RemoverAsync(VisitaTecnica visita);
        Task SalvarAlteracoesAsync();
    }
}
