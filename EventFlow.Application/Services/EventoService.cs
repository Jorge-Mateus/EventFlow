using EventFlow.Application.DTOs.Evento;
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
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _repository;

        public EventoService(
            IEventoRepository repository)
        {
            _repository = repository;
        }

        public async Task CriarAsync(
            CriarEventoDto dto)
        {
            var evento = new Evento(
                dto.ClienteId,
                dto.Nome,
                dto.DataEvento,
                dto.LocalEvento,
                dto.QuantidadeConvidados);

            await _repository
                .AdicionarAsync(evento);

            await _repository
                .SalvarAlteracoesAsync();
        }

        public async Task<IEnumerable<EventoDto>>
            ObterTodosAsync()
        {
            var eventos =
                await _repository.ObterTodosAsync();

            return eventos.Select(x =>
                new EventoDto
                {
                    Id = x.Id,
                    ClienteId = x.ClienteId,
                    Nome = x.Nome,
                    DataEvento = x.DataEvento,
                    LocalEvento = x.LocalEvento,
                    QuantidadeConvidados =
                        x.QuantidadeConvidados
                });
        }

        public async Task<EventoDto?>
            ObterPorIdAsync(Guid id)
        {
            var evento =
                await _repository.ObterPorIdAsync(id);

            if (evento is null)
                return null;

            return new EventoDto
            {
                Id = evento.Id,
                ClienteId = evento.ClienteId,
                Nome = evento.Nome,
                DataEvento = evento.DataEvento,
                LocalEvento = evento.LocalEvento,
                QuantidadeConvidados =
                    evento.QuantidadeConvidados
            };
        }
    }
}
