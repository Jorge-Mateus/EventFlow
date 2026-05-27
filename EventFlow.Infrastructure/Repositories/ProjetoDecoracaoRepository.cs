using Dapper;
using EventFlow.Domain.Entities;
using EventFlow.Domain.Interfaces;
using EventFlow.Infrastructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Infrastructure.Repositories
{
    public class ProjetoDecoracaoRepository : IProjetoDecoracaoRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public ProjetoDecoracaoRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private IDbConnection Connection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task AdicionarAsync(ProjetoDecoracao projeto)
        {
            await _context.ProjetosDecoracao.AddAsync(projeto);
        }

        public Task AtualizarAsync(ProjetoDecoracao projeto)
        {
            var existente = _context.ProjetosDecoracao.Local.FirstOrDefault(x => x.Id == projeto.Id);

            if (existente is not null)
            {
                _context.Entry(existente).State = EntityState.Detached;
            }

            _context.ProjetosDecoracao.Attach(projeto);

            _context.Entry(projeto).State = EntityState.Modified;

            return Task.CompletedTask;
        }

        public async Task<ProjetoDecoracao?> ObterPorIdAsync(Guid id)
        {
            var sql = @"
                SELECT *
                FROM ProjetosDecoracao
                WHERE Id = @Id;

                SELECT *
                FROM ProjetoArquivos
                WHERE ProjetoDecoracaoId = @Id;

                SELECT p.*
                FROM Propostas p
                INNER JOIN ProjetosDecoracao pd
                    ON pd.PropostaId = p.Id
                WHERE pd.Id = @Id;

                SELECT e.*
                FROM Eventos e
                INNER JOIN Propostas p
                    ON p.EventoId = e.Id
                INNER JOIN ProjetosDecoracao pd
                    ON pd.PropostaId = p.Id
                WHERE pd.Id = @Id;
            ";

            using var connection = Connection();

            using var multi = await connection.QueryMultipleAsync(sql, new { Id = id });

            var projeto = await multi.ReadFirstOrDefaultAsync<ProjetoDecoracao>();

            if (projeto is null) return null;

            var arquivos = (await multi.ReadAsync<ProjetoArquivo>()).ToList();

            foreach (var arquivo in arquivos)
            {
                projeto.AdicionarArquivo(arquivo);
            }

            var proposta = await multi.ReadFirstOrDefaultAsync<Proposta>();

            var evento = await multi.ReadFirstOrDefaultAsync<Evento>();

            if (proposta is not null)
            {
                proposta.DefinirEvento(evento);

                projeto.DefinirProposta(proposta);
            }

            return projeto;
        }

        public async Task<IEnumerable<ProjetoDecoracao>> ObterTodosAsync()
        {
            using var connection = Connection();

            return await connection.QueryAsync<ProjetoDecoracao>("SELECT * FROM ProjetosDecoracao");
        }

        public async Task RemoverAsync(ProjetoDecoracao projeto)
        {
            _context.ProjetosDecoracao.Remove(projeto);

            await _context.SaveChangesAsync();
        }

        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task RemoverArquivoAsync(Guid arquivoId)
        {
            var arquivo =
                await _context.ProjetoArquivos.FindAsync(arquivoId);

            if (arquivo is null)
                return;

            _context.ProjetoArquivos.Remove(arquivo);
        }

        public async Task AdicionarArquivoAsync(ProjetoArquivo arquivo)
        {
            await _context.ProjetoArquivos.AddAsync(arquivo);
        }
    }
}
