using EventFlow.Domain.Entities;

namespace EventFlow.Domain.Interfaces
{
    public interface IFornecedorRepository
    {
        Task AdicionarAsync(Fornecedor fornecedor);

        Task AtualizarAsync(Fornecedor fornecedor);

        Task<Fornecedor?> ObterPorIdAsync(Guid id);

        Task<IEnumerable<Fornecedor>> ObterTodosAsync();

        Task RemoverAsync(Fornecedor fornecedor);

        Task SalvarAlteracoesAsync();
    }
}
