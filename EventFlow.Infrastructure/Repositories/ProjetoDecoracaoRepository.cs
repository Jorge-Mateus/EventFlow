using Dapper;
using EventFlow.Domain.Entities;
using EventFlow.Domain.Interfaces;
using EventFlow.Infrastructure.Context;
using Microsoft.Data.SqlClient;
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
            _context.ProjetosDecoracao.Update(projeto);

            return Task.CompletedTask;
        }

        public async Task<ProjetoDecoracao?>
            ObterPorIdAsync(Guid id)
        {
            var sql = @"
            SELECT *
            FROM ProjetosDecoracao
            WHERE Id = @Id;

            SELECT *
            FROM ProjetoArquivos
            WHERE ProjetoDecoracaoId = @Id;
        ";

            using var connection = Connection();

            using var multi = await connection.QueryMultipleAsync(sql, new { Id = id });

            var projeto = await multi.ReadFirstOrDefaultAsync<ProjetoDecoracao>();

            if (projeto is null) return null;

            var arquivos = (await multi.ReadAsync<ProjetoArquivo>()).ToList();

            foreach (var arquivo in arquivos)
            {
                projeto.AdicionarArquivo(
                    arquivo);
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
    }
}
