using EventFlow.Application.DTOs.Contrato;
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
    public class ContratoService : IContratoService
    {
        private readonly IContratoRepository _repository;

        public ContratoService(IContratoRepository repository)
        {
            _repository = repository;
        }

        public async Task CriarAsync(CriarContratoDto dto)
        {
            var contrato = new Contrato(
                dto.EventoId,
                dto.Titulo,
                dto.Descricao,
                dto.CaminhoArquivo,
                dto.TipoArquivo);

            await _repository.AdicionarAsync(contrato);

            await _repository.SalvarAlteracoesAsync();
        }

        public async Task<IEnumerable<ContratoDto>> ObterPorEventoAsync(Guid eventoId)
        {
            var contratos = await _repository.ObterPorEventoAsync(eventoId);

            return contratos.Select(x => new ContratoDto
            {
                Id = x.Id,
                EventoId = x.EventoId,
                Titulo = x.Titulo,
                Descricao = x.Descricao,
                CaminhoArquivo = x.CaminhoArquivo,
                Assinado = x.Assinado,
                DataCriacao = x.DataCriacao,
                DataAssinatura = x.DataAssinatura
            });
        }

        public async Task MarcarAssinadoAsync(Guid id)
        {
            var contrato = await _repository.ObterPorIdAsync(id);

            if (contrato is null) return;

            contrato.MarcarComoAssinado();

            await _repository.AtualizarAsync(contrato);
            await _repository.SalvarAlteracoesAsync();
        }

        public async Task ExcluirAsync(Guid id)
        {
            var contrato = await _repository.ObterPorIdAsync(id);

            if (contrato is null)
                return;
            await _repository.RemoverAsync(contrato);
            await _repository.SalvarAlteracoesAsync();
        }
    }
}
