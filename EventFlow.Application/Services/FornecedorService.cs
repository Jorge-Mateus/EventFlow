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
    public class FornecedorService : IFornecedorService
    {
        private readonly IFornecedorRepository _repository;

        public FornecedorService(IFornecedorRepository repository)
        {
            _repository = repository;
        }

        public async Task CriarAsync(CriarFornecedorDto dto)
        {
            var fornecedor = new Fornecedor(dto.Nome, dto.Documento, dto.Telefone, dto.Email, dto.TipoServico);

            await _repository.AdicionarAsync(fornecedor);

            await _repository.SalvarAlteracoesAsync();
        }

        public async Task AtualizarAsync(AtualizarFornecedorDto dto)
        {
            var fornecedor = await _repository.ObterPorIdAsync(dto.Id);

            if (fornecedor is null) return;

            fornecedor.Atualizar(dto.Nome, dto.Documento, dto.Telefone, dto.Email, dto.TipoServico);

            await _repository.AtualizarAsync(fornecedor);

            await _repository.SalvarAlteracoesAsync();
        }

        public async Task<IEnumerable<FornecedorDto>> ObterTodosAsync()
        {
            var fornecedores = await _repository.ObterTodosAsync();

            return fornecedores.Select(x => new FornecedorDto
            {
                Id = x.Id,
                Nome = x.Nome,
                Documento = x.Documento,
                Telefone = x.Telefone,
                Email = x.Email,
                TipoServico = x.TipoServico
            });
        }

        public async Task<FornecedorDto?> ObterPorIdAsync(Guid id)
        {
            var fornecedor = await _repository.ObterPorIdAsync(id);

            if (fornecedor is null) return null;

            return new FornecedorDto
            {
                Id = fornecedor.Id,
                Nome = fornecedor.Nome,
                Documento = fornecedor.Documento,
                Telefone = fornecedor.Telefone,
                Email = fornecedor.Email,
                TipoServico = fornecedor.TipoServico
            };
        }

        public async Task ExcluirAsync(Guid id)
        {
            var fornecedor = await _repository.ObterPorIdAsync(id);

            if (fornecedor is null) return;

            await _repository.RemoverAsync(fornecedor);

            await _repository.SalvarAlteracoesAsync();
        }
    }
}
