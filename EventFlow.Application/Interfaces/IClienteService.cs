using EventFlow.Application.DTOs.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Interfaces
{
    public interface IClienteService
    {
        Task CriarAsync(CriarClienteDto dto);
        Task<IEnumerable<ClienteDto>> ObterTodosAsync();
        Task<ClienteDto?> ObterPorIdAsync(Guid id);
        Task<ClienteDetalheDto?> ObterDetalheAsync(Guid id);
        Task AtualizarAsync(AtualizarClienteDto dto);
        Task ExcluirAsync(Guid id);
    }
}
