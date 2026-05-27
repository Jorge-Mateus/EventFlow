using EventFlow.Application.DTOs.Colaborador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Interfaces
{
    public interface IColaboradorService
    {
        Task CriarAsync(CriarColaboradorDto dto);
        Task AtualizarAsync(AtualizarColaboradorDto dto);
        Task<ColaboradorDto?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<ColaboradorDto>> ObterTodosAsync();
        Task RemoverAsync(Guid id);
    }
}
