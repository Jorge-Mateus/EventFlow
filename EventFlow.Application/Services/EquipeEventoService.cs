using EventFlow.Application.DTOs.EquipeEvento;
using EventFlow.Application.Interfaces;
using EventFlow.Domain.Entities;
using EventFlow.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Services
{

    public class EquipeEventoService : IEquipeEventoService
    {
        private readonly IEquipeEventoRepository _repository;
        private readonly IColaboradorRepository _colaboradorRepository;

        public EquipeEventoService(IEquipeEventoRepository repository, IColaboradorRepository colaboradorRepository)
        {
            _repository = repository;
            _colaboradorRepository = colaboradorRepository;
        }

        public async Task CriarAsync(CriarEquipeEventoDto dto)
        {
            foreach (var item in dto.Itens)
            {
                var equipe = new EquipeEvento(
                    dto.EventoId,
                    item.ColaboradorId,
                    item.ValorPagamento);

                await _repository.AdicionarAsync(equipe);
            }

            await _repository.SalvarAlteracoesAsync();
        }

        public async Task AtualizarAsync( CriarEquipeEventoDto dto)
        {
            var equipeAtual =  await _repository.ObterPorEventoAsync(dto.EventoId);

            foreach (var item in equipeAtual)
            {
                await _repository.RemoverAsync(item);
            }

            foreach (var item in dto.Itens)
            {
                var equipe = new EquipeEvento(
                    dto.EventoId,
                    item.ColaboradorId,
                    item.ValorPagamento);

                await _repository.AdicionarAsync(equipe);
            }

            await _repository.SalvarAlteracoesAsync();
        }

        public async Task<EquipeEventoDto?> ObterPorIdAsync(Guid id)
        {
            var equipe = await _repository.ObterPorIdAsync(id);

            if (equipe is null) return null;

            return new EquipeEventoDto
            {
                Id = equipe.Id,
                EventoId = equipe.EventoId,
                ColaboradorId = equipe.ColaboradorId,
                EventoNome = equipe.Evento?.Nome,
                ValorPagamento = equipe.ValorPagamento
            };
        }

        public async Task<IEnumerable<EquipeEventoDto>> ObterTodosAsync()
        {
            var equipes = await _repository.ObterTodosAsync();

            return equipes.Select(x =>
                new EquipeEventoDto
                {
                    Id = x.Id,
                    EventoId = x.EventoId,
                    ColaboradorId = x.ColaboradorId,
                    ValorPagamento = x.ValorPagamento
                });
        }

        public async Task<IEnumerable<EquipeEventoDto>> ObterPorEventoAsync(Guid eventoId)
        {
            var equipes = await _repository.ObterPorEventoAsync(eventoId);

            return equipes.Select(x =>
                new EquipeEventoDto
                {
                    Id = x.Id,
                    EventoId = x.EventoId,
                    ColaboradorId = x.ColaboradorId,
                    ValorPagamento = x.ValorPagamento
                });
        }

        public async Task RemoverAsync(Guid id)
        {
            var equipe = await _repository.ObterPorIdAsync(id);

            if (equipe is null) return;

            await _repository.RemoverAsync(equipe);

            await _repository.SalvarAlteracoesAsync();
        }


    }
}
