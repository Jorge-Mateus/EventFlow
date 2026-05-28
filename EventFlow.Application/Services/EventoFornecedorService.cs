using EventFlow.Application.DTOs.Fornecedor;
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
    public class EventoFornecedorService : IEventoFornecedorService
    {
        private readonly IEventoFornecedorRepository _repository;

        public EventoFornecedorService(IEventoFornecedorRepository repository)
        {
            _repository = repository;
        }

        public async Task CriarAsync(CriarEventoFornecedorDto dto)
        {
            foreach (var item in dto.Itens)
            {
                var eventoFornecedor = new EventoFornecedor(
                        dto.EventoId,
                        item.FornecedorId,
                        item.ValorContratado);

                await _repository.AdicionarAsync(eventoFornecedor);
            }

            await _repository.SalvarAlteracoesAsync();
        }

        public async Task AtualizarAsync(CriarEventoFornecedorDto dto)
        {
            var existentes = await _repository.ObterPorEventoAsync(dto.EventoId);
            foreach (var item in existentes)
            {
                await _repository.RemoverAsync(item);
            }

            foreach (var item in dto.Itens)
            {
                await _repository.AdicionarAsync(
                    new EventoFornecedor(
                        dto.EventoId,
                        item.FornecedorId,
                        item.ValorContratado));
            }

            await _repository.SalvarAlteracoesAsync();
        }

        public async Task<IEnumerable<CriarEventoFornecedorItemDto>> ObterPorEventoAsync(Guid eventoId)
        {
            var itens = await _repository.ObterPorEventoAsync(eventoId);

            return itens.Select(x => new CriarEventoFornecedorItemDto
            {
                FornecedorId = x.FornecedorId,
                ValorContratado = x.ValorContratado
            });
        }
    }
}
