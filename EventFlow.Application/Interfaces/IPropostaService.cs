using EventFlow.Application.DTOs.Proposta;
using EventFlow.Domain.Enums;

namespace EventFlow.Application.Interfaces
{
    public interface IPropostaService
    {
        Task CriarAsync(CriarPropostaDto dto);
        Task<IEnumerable<PropostaDto>> ObterTodosAsync();
        Task<PropostaDto?> ObterPorIdAsync(Guid id);
        Task<PropostaDetalheDto?> ObterDetalheAsync(Guid id);
        Task AtualizarAsync(AtualizarPropostaDto dto);
        Task ExcluirAsync(Guid id);
        Task AlterarStatusAsync(Guid id, StatusProposta status);
    }
}
