using EventFlow.Application.DTOs.CategoriaOrcamento;
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
    public class CategoriaOrcamentoService
    : ICategoriaOrcamentoService
    {
        private readonly
            ICategoriaOrcamentoRepository
            _repository;

        public CategoriaOrcamentoService(
            ICategoriaOrcamentoRepository repository)
        {
            _repository = repository;
        }

        public async Task CriarAsync(
            CriarCategoriaOrcamentoDto dto)
        {
            var categoria =
                new CategoriaOrcamento(
                    dto.Nome);

            await _repository
                .AdicionarAsync(categoria);

            await _repository
                .SalvarAlteracoesAsync();
        }

        public async Task<
            IEnumerable<CategoriaOrcamentoDto>>
            ObterTodosAsync()
        {
            var categorias =
                await _repository
                    .ObterTodosAsync();

            return categorias.Select(x =>
                new CategoriaOrcamentoDto
                {
                    Id = x.Id,
                    Nome = x.Nome
                });
        }
    }
}
