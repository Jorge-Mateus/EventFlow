using EventFlow.Application.DTOs.Proposta;
using EventFlow.Application.Interfaces;
using EventFlow.Domain.Entities;
using EventFlow.Domain.Enums;
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
            dto.EventoId);

        foreach (var item in dto.Itens)
        {
            proposta.AdicionarItem(
                item.CategoriaOrcamentoId,
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
                EventoId = x.EventoId,
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
            EventoId = proposta.EventoId,
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

    public async Task<PropostaDetalheDto?> ObterDetalheAsync(Guid id)
    {
        var proposta = await _repository.ObterPorIdAsync(id);

        if (proposta is null)
            return null;

        return new PropostaDetalheDto
        {
            Id = proposta.Id,
            Status = proposta.Status.ToString(),
            ValorTotal = proposta.ValorTotal,
            Itens = proposta.Itens.Select(x =>
                new PropostaItemDto
                {
                    Id = x.Id,
                    Descricao = x.Descricao,
                    Quantidade = x.Quantidade,
                    ValorUnitario = x.ValorUnitario,
                    Total = x.Total
                }).ToList()
        };
    }

    public async Task AtualizarAsync(AtualizarPropostaDto dto)
    {
        var proposta = await _repository.ObterPorIdAsync(dto.Id);

        if (proposta is null)
            return;

        proposta.Atualizar(dto.EventoId, (StatusProposta)dto.Status);

        await _repository.AtualizarAsync(proposta);

        await _repository.SalvarAlteracoesAsync();
    }

    public async Task ExcluirAsync(Guid id)
    {
        var proposta = await _repository.ObterPorIdAsync(id);

        if (proposta is null)
            return;

        await _repository.RemoverAsync(proposta);

        await _repository.SalvarAlteracoesAsync();
    }
}