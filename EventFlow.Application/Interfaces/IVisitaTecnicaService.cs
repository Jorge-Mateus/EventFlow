using EventFlow.Application.DTOs.VisitaTecnica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Interfaces
{
    public interface IVisitaTecnicaService
    {
        Task CriarAsync(CriarVisitaTecnicaDto dto);
        Task AtualizarAsync(AtualizarVisitaTecnicaDto dto);
        Task<VisitaTecnicaDto?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<VisitaTecnicaDto>> ObterTodosAsync();
        Task ExcluirAsync(Guid id);
    }
}
