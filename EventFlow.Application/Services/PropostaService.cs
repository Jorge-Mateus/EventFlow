using EventFlow.Application.DTOs.Proposta;
using EventFlow.Application.Interfaces;
using EventFlow.Domain.Entities;
using EventFlow.Domain.Interfaces;

namespace EventFlow.Application.Services;

public class PropostaService : IPropostaService
{
    private readonly IPropostaRepository _repository;

    public PropostaService(
        IPropostaRepository repository)
    {
        _repository = repository;
    }

    public async Task CriarAsync(
        CriarPropostaDto dto)
    {
        var proposta = new Proposta(
            dto.ClienteId);

        foreach (var item in dto.Itens)
        {
            proposta.AdicionarItem(
                item.Descricao,
                item.Quantidade,
                item.ValorUnitario);
        }

        await _repository.AdicionarAsync(
            proposta);

        await _repository.SalvarAlteracoesAsync();
    }

    public async Task<IEnumerable<PropostaDto>>
        ObterTodosAsync()
    {
        var propostas =
            await _repository.ObterTodosAsync();

        return propostas.Select(x =>
            new PropostaDto
            {
                Id = x.Id,
                ClienteId = x.ClienteId,
                Status = x.Status,
                ValorTotal = x.ValorTotal
            });
    }

    public async Task<PropostaDto?>
        ObterPorIdAsync(Guid id)
    {
        var proposta =
            await _repository.ObterPorIdAsync(id);

        if (proposta is null)
            return null;

        return new PropostaDto
        {
            Id = proposta.Id,
            ClienteId = proposta.ClienteId,
            Status = proposta.Status,
            ValorTotal = proposta.ValorTotal,

            Itens = proposta.Itens
                .Select(item => new PropostaItemDto
                {
                    Descricao = item.Descricao,
                    Quantidade = item.Quantidade,
                    ValorUnitario = item.ValorUnitario,
                    Total = item.Total
                })
                .ToList()
        };
    }
}