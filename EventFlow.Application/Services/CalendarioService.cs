using EventFlow.Application.DTOs.Calendario;
using EventFlow.Application.Interfaces;
using EventFlow.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Services
{
    public class CalendarioService : ICalendarioService
    {
        private readonly IEventoRepository _repository;

        public CalendarioService(
            IEventoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CalendarioEventoDto>> ObterAsync()
        {
            var eventos =
                await _repository.ObterTodosAsync();

            return eventos
                .OrderBy(x => x.DataEvento)
                .Select(x => new CalendarioEventoDto
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    DataEvento = x.DataEvento,
                    LocalEvento = x.LocalEvento,
                    TemEquipe = x.TemEquipe,
                    TemFornecedor = x.TemFornecedor
                });
        }
    }
}