using EventFlow.Application.DTOs.ProjetoDecoracao;
using EventFlow.Application.Interfaces;
using EventFlow.Domain.Entities;
using EventFlow.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
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
                projeto.AdicionarArquivo(new ProjetoArquivo(projeto.Id, dto.NomeArquivo, dto.CaminhoArquivo!, dto.TipoArquivo!));
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
                Observacoes = projeto.Observacoes,

                NomeEvento = projeto.Proposta.Evento.Nome,
                DataEvento = projeto.Proposta.Evento.DataEvento,

                StatusProposta = projeto.Proposta.Status.ToString(),

                Arquivos = projeto.Arquivos.Select(x => new ProjetoArquivoDto
                {
                    Id = x.Id,
                    NomeArquivo = x.NomeArquivo,
                    Caminho = x.Caminho,
                    Tipo = x.Tipo
                }).ToList()
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

            var arquivo = new ProjetoArquivo(projeto.Id, dto.NomeArquivo, dto.Caminho, dto.Tipo);

            projeto.AdicionarArquivo(arquivo);

            await _repository.AtualizarAsync(projeto);

            await _repository.SalvarAlteracoesAsync();
        }
        public async Task<ProjetoDecoracaoDto?> ObterDetalheAsync(Guid id)
        {
            return await ObterPorIdAsync(id);
        }
        public async Task AtualizarAsync(AtualizarProjetoDecoracaoDto dto)
        {
            var projeto = await _repository.ObterPorIdAsync(dto.Id);

            if (projeto is null)
                return;

            projeto.Atualizar(dto.PropostaId, dto.Nome, dto.Observacoes);

            projeto.LimparArquivos();

            await _repository.AtualizarAsync(projeto);

            await _repository.SalvarAlteracoesAsync();
        }
        public async Task ExcluirAsync(Guid id)
        {
            var projeto = await _repository.ObterPorIdAsync(id);

            if (projeto is null) return;

            await _repository.RemoverAsync(projeto);
            await _repository.SalvarAlteracoesAsync();
        }
        public async Task AdicionarArquivosAsync(Guid projetoId, List<(string Nome, string Caminho, string Tipo)> arquivos)
        {
            foreach (var arquivo in arquivos)
            {
                await _repository.AdicionarArquivoAsync(
                    new ProjetoArquivo(
                        projetoId,
                        arquivo.Nome,
                        arquivo.Caminho,
                        arquivo.Tipo));
            }

            await _repository.SalvarAlteracoesAsync();
        }

        public async Task AtualizarComArquivosAsync(AtualizarProjetoDecoracaoDto dto, List<(string Nome, string Caminho, string Tipo)> arquivos)
        {
            var projeto = await _repository.ObterPorIdAsync(dto.Id);

            if (projeto is null) return;

            projeto.Atualizar(dto.PropostaId, dto.Nome, dto.Observacoes);
            projeto.LimparArquivos();

            foreach (var arquivo in arquivos)
            {
                projeto.AdicionarArquivo(
                    new ProjetoArquivo(
                        projeto.Id,
                        arquivo.Nome,
                        arquivo.Caminho,
                        arquivo.Tipo));
            }

            await _repository.AtualizarAsync(projeto);

            await _repository.SalvarAlteracoesAsync();
        }
        public async Task RemoverArquivoAsync(Guid arquivoId)
        {
            await _repository.RemoverArquivoAsync(arquivoId);

            await _repository.SalvarAlteracoesAsync();
        }
    }
}
