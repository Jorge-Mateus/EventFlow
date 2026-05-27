using EventFlow.Application.DTOs.Funcao;
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
    public class FuncaoService : IFuncaoService
    {
        private readonly IFuncaoRepository _repository;

        public FuncaoService(IFuncaoRepository repository)
        {
            _repository = repository;
        }

        public async Task CriarAsync(CriarFuncaoDto dto)
        {
            var funcao = new Funcao(
                dto.Nome);

            await _repository.AdicionarAsync(funcao);

            await _repository.SalvarAlteracoesAsync();
        }

        public async Task AtualizarAsync(AtualizarFuncaoDto dto)
        {
            var funcao = await _repository.ObterPorIdAsync(dto.Id);

            if (funcao is null) return;

            funcao.Atualizar(dto.Nome);

            await _repository.AtualizarAsync(funcao);

            await _repository.SalvarAlteracoesAsync();
        }

        public async Task<FuncaoDto?> ObterPorIdAsync(Guid id)
        {
            var funcao = await _repository.ObterPorIdAsync(id);

            if (funcao is null) return null;

            return new FuncaoDto
            {
                Id = funcao.Id,
                Nome = funcao.Nome
            };
        }

        public async Task<IEnumerable<FuncaoDto>> ObterTodosAsync()
        {
            var funcoes = await _repository.ObterTodosAsync();

            return funcoes.Select(x =>
                new FuncaoDto
                {
                    Id = x.Id,
                    Nome = x.Nome
                });
        }

        public async Task RemoverAsync(Guid id)
        {
            var funcao = await _repository.ObterPorIdAsync(id);

            if (funcao is null) return;

            await _repository.RemoverAsync(funcao);

            await _repository.SalvarAlteracoesAsync();
        }
    }
}
