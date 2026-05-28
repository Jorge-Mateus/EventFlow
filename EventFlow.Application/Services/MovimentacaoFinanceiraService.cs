using EventFlow.Application.DTOs.MovimentacaoFinanceira;
using EventFlow.Application.Interfaces;
using EventFlow.Domain.Entities;
using EventFlow.Domain.Enums;
using EventFlow.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Services
{
    public class MovimentacaoFinanceiraService : IMovimentacaoFinanceiraService
    {
        private readonly IMovimentacaoFinanceiraRepository _repository;

        public MovimentacaoFinanceiraService(IMovimentacaoFinanceiraRepository repository)
        {
            _repository = repository;
        }

        public async Task CriarAsync(CriarMovimentacaoFinanceiraDto dto)
        {
            var movimentacao = new MovimentacaoFinanceira(
                    dto.EventoId,
                    dto.Descricao,
                    dto.Valor,
                    dto.Tipo);

            await _repository.AdicionarAsync(movimentacao);

            await _repository.SalvarAlteracoesAsync();
        }

        public async Task AtualizarAsync(AtualizarMovimentacaoFinanceiraDto dto)
        {
            var movimentacao = await _repository.ObterPorIdAsync(dto.Id);

            if (movimentacao is null)
                return;

            movimentacao.Atualizar(dto.Descricao, dto.Valor, dto.Tipo);

            await _repository.AtualizarAsync(movimentacao);

            await _repository.SalvarAlteracoesAsync();
        }

        public async Task<IEnumerable<MovimentacaoFinanceiraDto>> ObterPorEventoAsync(Guid eventoId)
        {
            var movimentacoes = await _repository.ObterPorEventoAsync(eventoId);

            return movimentacoes.Select(x =>
                new MovimentacaoFinanceiraDto
                {
                    Id = x.Id,
                    EventoId = x.EventoId,
                    Descricao = x.Descricao,
                    Valor = x.Valor,
                    DataMovimentacao = x.DataMovimentacao,
                    Tipo = x.Tipo
                });
        }

        public async Task<ResumoFinanceiroDto> ObterResumoAsync(Guid eventoId)
        {
            var movimentacoes = await _repository.ObterPorEventoAsync(
                    eventoId);

            var entradas = movimentacoes
                .Where(x => x.Tipo == TipoMovimentacao.Entrada)
                .Sum(x => x.Valor);

            var saidas = movimentacoes
                .Where(x => x.Tipo == TipoMovimentacao.Saida)
                .Sum(x => x.Valor);

            return new ResumoFinanceiroDto
            {
                TotalEntradas = entradas,
                TotalSaidas = saidas,
                LucroLiquido = entradas - saidas
            };
        }

        public async Task ExcluirAsync(Guid id)
        {
            var movimentacao = await _repository.ObterPorIdAsync(id);

            if (movimentacao is null) return;

            await _repository.RemoverAsync(movimentacao);

            await _repository.SalvarAlteracoesAsync();
        }
    }
}
