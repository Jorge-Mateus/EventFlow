using EventFlow.Application.DTOs.Proposta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Interfaces
{
    public interface IPropostaService
    {
        Task CriarAsync(CriarPropostaDto dto);

        Task<IEnumerable<PropostaDto>>
            ObterTodosAsync();

        Task<PropostaDto?>
            ObterPorIdAsync(Guid id);
    }
}
