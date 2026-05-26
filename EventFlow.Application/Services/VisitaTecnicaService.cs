using EventFlow.Application.DTOs.VisitaTecnica;
using EventFlow.Application.Interfaces;
using EventFlow.Domain.Entities;
using EventFlow.Domain.Interfaces;
using EventFlow.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Services
{
    public class VisitaTecnicaService : IVisitaTecnicaService
    {
        private readonly IVisitaTecnicaRepository _repository;
        private readonly IPropostaRepository _propostaRepository;


        public VisitaTecnicaService(IVisitaTecnicaRepository repository, IPropostaRepository propostaRepository)
        {
            _repository = repository;
            _propostaRepository = propostaRepository;
        }

        public async Task CriarAsync(CriarVisitaTecnicaDto dto)
        {
            var visita = new VisitaTecnica(dto.PropostaId, dto.DataAgendada, dto.Responsavel, dto.Observacoes);

            await _repository.AdicionarAsync(visita);

            var proposta = await _propostaRepository.ObterPorIdAsync(dto.PropostaId);

            if (proposta is not null)
            {
                proposta.AgendarVisitaTecnica();

                await _propostaRepository.AtualizarAsync(proposta);
            }

            await _repository.SalvarAlteracoesAsync();
        }

        public async Task AtualizarAsync(AtualizarVisitaTecnicaDto dto)
        {
            var visita = await _repository
                    .ObterPorIdAsync(dto.Id);

            if (visita is null) return;

            visita.Atualizar(dto.DataAgendada, dto.Responsavel, dto.Observacoes);

            await _repository.AtualizarAsync(visita);

            await _repository.SalvarAlteracoesAsync();
        }

        public async Task<VisitaTecnicaDto?> ObterPorIdAsync(Guid id)
        {
            var visita = await _repository.ObterPorIdAsync(id);

            if (visita is null) return null;

            return new VisitaTecnicaDto
            {
                Id = visita.Id,
                PropostaId = visita.PropostaId,
                DataAgendada = visita.DataAgendada,
                Responsavel = visita.Responsavel,
                Observacoes = visita.Observacoes
            };
        }

        public async Task<IEnumerable<VisitaTecnicaDto>> ObterTodosAsync()
        {
            var visitas = await _repository.ObterTodosAsync();

            return visitas.Select(x =>
                new VisitaTecnicaDto
                {
                    Id = x.Id,
                    PropostaId = x.PropostaId,
                    DataAgendada = x.DataAgendada,
                    Responsavel = x.Responsavel,
                    Observacoes = x.Observacoes
                });
        }

        public async Task ExcluirAsync(Guid id)
        {
            var visita = await _repository.ObterPorIdAsync(id);

            if (visita is null) return;

            await _repository.RemoverAsync(visita);
        }
    }
}
