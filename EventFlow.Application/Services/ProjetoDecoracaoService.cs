using EventFlow.Application.DTOs.ProjetoDecoracao;
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
    public class ProjetoDecoracaoService : IProjetoDecoracaoService
    {
        private readonly IProjetoDecoracaoRepository _repository;
        public ProjetoDecoracaoService(IProjetoDecoracaoRepository repository)
        {
            _repository = repository;
        }

        public async Task CriarAsync(CriarProjetoDecoracaoDto dto)
        {
            var projeto = new ProjetoDecoracao(dto.PropostaId, dto.Nome, dto.Observacoes);

            if (!string.IsNullOrWhiteSpace(dto.NomeArquivo))
            {
                projeto.AdicionarArquivo(new ProjetoArquivo(dto.NomeArquivo, dto.CaminhoArquivo!, dto.TipoArquivo!));
            }

            await _repository.AdicionarAsync(projeto);
            await _repository.SalvarAlteracoesAsync();
        }

        public async Task<ProjetoDecoracaoDto?> ObterPorIdAsync(Guid id)
        {
            var projeto = await _repository.ObterPorIdAsync(id);

            if (projeto is null) return null;

            return new ProjetoDecoracaoDto
            {
                Id = projeto.Id,
                PropostaId = projeto.PropostaId,
                Nome = projeto.Nome,
                Observacoes = projeto.Observacoes
            };
        }

        public async Task<IEnumerable<ProjetoDecoracaoDto>> ObterTodosAsync()
        {
            var projetos = await _repository.ObterTodosAsync();

            return projetos.Select(x => new ProjetoDecoracaoDto
            {
                Id = x.Id,
                PropostaId = x.PropostaId,
                Nome = x.Nome,
                Observacoes = x.Observacoes
            });
        }
        public async Task UploadArquivoAsync(UploadProjetoArquivoDto dto)
        {
            var projeto = await _repository.ObterPorIdAsync(dto.ProjetoDecoracaoId);

            if (projeto is null) throw new Exception("Projeto não encontrado");

            var arquivo = new ProjetoArquivo(dto.NomeArquivo, dto.Caminho, dto.Tipo);

            projeto.AdicionarArquivo(arquivo);

            await _repository.AtualizarAsync(projeto);

            await _repository.SalvarAlteracoesAsync();
        }
    }
}
