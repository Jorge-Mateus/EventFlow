using EventFlow.Domain.Entities;

namespace EventFlow.Domain.Interfaces;

public interface ICategoriaOrcamentoRepository
{
    Task AdicionarAsync(
        CategoriaOrcamento categoria);

    Task AtualizarAsync(
        CategoriaOrcamento categoria);

    Task<CategoriaOrcamento?>
        ObterPorIdAsync(Guid id);

    Task<IEnumerable<CategoriaOrcamento>>
        ObterTodosAsync();

    Task SalvarAlteracoesAsync();
}