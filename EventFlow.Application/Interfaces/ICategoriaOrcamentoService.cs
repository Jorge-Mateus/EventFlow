using EventFlow.Application.DTOs.CategoriaOrcamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Interfaces
{
    public interface ICategoriaOrcamentoService
    {
        Task CriarAsync(CriarCategoriaOrcamentoDto dto);
        Task<IEnumerable<CategoriaOrcamentoDto>> ObterTodosAsync();
        Task<CategoriaOrcamentoDetalheDto?> ObterDetalheAsync(Guid id);
        Task AtualizarAsync(AtualizarCategoriaOrcamentoDto dto);
        Task ExcluirAsync(Guid id);
    }
}
