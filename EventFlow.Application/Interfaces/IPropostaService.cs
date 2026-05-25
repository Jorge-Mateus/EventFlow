using EventFlow.Application.DTOs.Proposta;

namespace EventFlow.Application.Interfaces
{
    public interface IPropostaService
    {
        Task CriarAsync(CriarPropostaDto dto);
        Task<IEnumerable<PropostaDto>> ObterTodosAsync();
        Task<PropostaDto?> ObterPorIdAsync(Guid id);
        Task<PropostaDetalheDto?>ObterDetalheAsync(Guid id);
        Task AtualizarAsync(AtualizarPropostaDto dto);
        Task ExcluirAsync(Guid id);
    }
}
