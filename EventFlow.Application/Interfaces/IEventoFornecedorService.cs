using EventFlow.Application.DTOs.Fornecedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Interfaces
{
    public interface IEventoFornecedorService
    {
        Task CriarAsync(CriarEventoFornecedorDto dto);
        Task AtualizarAsync(CriarEventoFornecedorDto dto);
        Task<IEnumerable<CriarEventoFornecedorItemDto>>ObterPorEventoAsync(Guid eventoId);
    }
}
