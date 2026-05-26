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

    public async Task CriarAsync(CriarPropostaDto dto)
    {
        var proposta = new Proposta(dto.EventoId);

        foreach (var categoriaDto in dto.Categorias)
        {
            proposta.AdicionarCategoria(categoriaDto.CategoriaOrcamentoId, categoriaDto.Valor);

            var categoria = proposta.Categorias.Last();

            foreach (var itemDto in categoriaDto.Itens)
            {
                categoria.AdicionarItem(itemDto.Descricao);
            }
        }

        await _repository.AdicionarAsync(proposta);

        await _repository.SalvarAlteracoesAsync();
    }

    public async Task<IEnumerable<PropostaDto>> ObterTodosAsync()
    {
        var propostas = await _repository.ObterTodosAsync();

        return propostas.Select(x =>
            new PropostaDto
            {
                Id = x.Id,
                EventoId = x.EventoId,
                Status = x.Status,
                ValorTotal = x.ValorTotal
            });
    }

    public async Task<PropostaDto?> ObterPorIdAsync(
    Guid id)
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

            Categorias = proposta.Categorias
                .Select(categoria =>
                    new PropostaCategoriaDto
                    {
                        CategoriaOrcamentoId =
                            categoria.CategoriaOrcamentoId,

                        Valor = categoria.Valor,

                        Itens = categoria.Itens
                            .Select(item =>
                                new PropostaCategoriaItemDto
                                {
                                    Descricao =
                                        item.Descricao
                                })
                            .ToList()
                    })
                .ToList()
        };
    }

    public async Task<PropostaDetalheDto?>
    ObterDetalheAsync(Guid id)
    {
        var proposta =
            await _repository.ObterPorIdAsync(id);

        if (proposta is null)
            return null;

        return new PropostaDetalheDto
        {
            Id = proposta.Id,
            Status = proposta.Status.ToString(),
            ValorTotal = proposta.ValorTotal,

            Categorias = proposta.Categorias
                .Select(categoria =>
                    new PropostaCategoriaDetalheDto
                    {
                        Id = categoria.Id,

                        CategoriaOrcamentoId =
                            categoria.CategoriaOrcamentoId,

                        Valor = categoria.Valor,

                        Itens = categoria.Itens
                            .Select(item =>
                                new PropostaCategoriaItemDto
                                {
                                    Id = item.Id,
                                    Descricao =
                                        item.Descricao
                                }).ToList()
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

    public async Task AlterarStatusAsync(Guid id, StatusProposta status)
    {
        var proposta = await _repository.ObterPorIdAsync(id);

        if (proposta is null) throw new Exception("Proposta não encontrada.");

        switch (status)
        {
            case StatusProposta.Enviada:
                proposta.Enviar();
                break;

            case StatusProposta.EmAjuste:
                proposta.SolicitarAjuste();
                break;

            case StatusProposta.VisitaTecnicaAgendada:
                proposta.AgendarVisitaTecnica();
                break;

            case StatusProposta.EmProjeto3D:
                proposta.IniciarProjeto3D();
                break;

            case StatusProposta.ProjetoAprovado:
                proposta.AprovarProjeto();
                break;

            case StatusProposta.EmMontagem:
                proposta.IniciarMontagem();
                break;

            case StatusProposta.Finalizada:
                proposta.Finalizar();
                break;

            case StatusProposta.Cancelada:
                proposta.Cancelar();
                break;
        }

        await _repository.AtualizarAsync(proposta);

        await _repository.SalvarAlteracoesAsync();
    }
}