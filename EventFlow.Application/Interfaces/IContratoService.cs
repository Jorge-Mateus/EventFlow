using EventFlow.Application.DTOs.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Interfaces
{
    public interface IContratoService
    {
        Task CriarAsync(CriarContratoDto dto);
        Task<IEnumerable<ContratoDto>> ObterPorEventoAsync(Guid eventoId);
        Task MarcarAssinadoAsync(Guid id);
        Task ExcluirAsync(Guid id);
    }
}
