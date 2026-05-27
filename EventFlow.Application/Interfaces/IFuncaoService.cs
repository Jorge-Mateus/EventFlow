using EventFlow.Application.DTOs.Funcao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Interfaces
{
    public interface IFuncaoService
    {
        Task CriarAsync(CriarFuncaoDto dto);
        Task AtualizarAsync(AtualizarFuncaoDto dto);
        Task<FuncaoDto?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<FuncaoDto>> ObterTodosAsync();
        Task RemoverAsync(Guid id);
    }
}
