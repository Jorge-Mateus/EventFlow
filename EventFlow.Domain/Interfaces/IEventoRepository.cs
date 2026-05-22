using EventFlow.Domain.Entities;

namespace EventFlow.Domain.Interfaces;

public interface IEventoRepository
{
    Task AdicionarAsync(Evento evento);
    Task AtualizarAsync(Evento evento);
    Task<Evento?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Evento>> ObterTodosAsync();
    Task SalvarAlteracoesAsync();
}