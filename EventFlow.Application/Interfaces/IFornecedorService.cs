using EventFlow.Application.DTOs.Fornecedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Interfaces
{
    public interface IFornecedorService
    {
        Task CriarAsync(CriarFornecedorDto dto);
        Task AtualizarAsync(AtualizarFornecedorDto dto);
        Task<IEnumerable<FornecedorDto>> ObterTodosAsync();
        Task<FornecedorDto?> ObterPorIdAsync(Guid id);
        Task ExcluirAsync(Guid id);
    }
}
