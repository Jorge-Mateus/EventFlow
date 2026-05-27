using EventFlow.Application.DTOs.Colaborador;
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
    public class ColaboradorService : IColaboradorService
    {
        private readonly IColaboradorRepository _repository;

        public ColaboradorService(IColaboradorRepository repository)
        {
            _repository = repository;
        }

        public async Task CriarAsync(CriarColaboradorDto dto)
        {
            var colaborador = new Colaborador(
                dto.Nome,
                dto.Telefone,
                dto.CPF,
                dto.Pix,
                dto.FuncaoId);

            await _repository.AdicionarAsync(colaborador);

            await _repository.SalvarAlteracoesAsync();
        }

        public async Task AtualizarAsync(AtualizarColaboradorDto dto)
        {
            var colaborador = await _repository.ObterPorIdAsync(dto.Id);

            if (colaborador is null) return;

            colaborador.Atualizar(
                dto.Nome,
                dto.Telefone,
                dto.CPF,
                dto.Pix,
                dto.FuncaoId);

            await _repository.AtualizarAsync(colaborador);

            await _repository.SalvarAlteracoesAsync();
        }

        public async Task<ColaboradorDto?> ObterPorIdAsync(Guid id)
        {
            var colaborador = await _repository.ObterPorIdAsync(id);

            if (colaborador is null) return null;

            return new ColaboradorDto
            {
                Id = colaborador.Id,
                Nome = colaborador.Nome,
                Telefone = colaborador.Telefone,
                CPF = colaborador.CPF,
                Pix = colaborador.Pix,
                FuncaoId = colaborador.FuncaoId,
                Ativo = colaborador.Ativo
            };
        }

        public async Task<IEnumerable<ColaboradorDto>> ObterTodosAsync()
        {
            var colaboradores = await _repository.ObterTodosAsync();

            return colaboradores.Select(x =>
                new ColaboradorDto
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Telefone = x.Telefone,
                    CPF = x.CPF,
                    Pix = x.Pix,
                    FuncaoId = x.FuncaoId,
                    Ativo = x.Ativo
                });
        }

        public async Task RemoverAsync(Guid id)
        {
            var colaborador = await _repository.ObterPorIdAsync(id);

            if (colaborador is null) return;

            await _repository.RemoverAsync(colaborador);

            await _repository.SalvarAlteracoesAsync();
        }
    }
}
