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
    public class FuncaoRepository : IFuncaoRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public FuncaoRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private IDbConnection Connection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task AdicionarAsync(Funcao funcao)
        {
            await _context.Funcoes.AddAsync(funcao);
        }

        public Task AtualizarAsync(Funcao funcao)
        {
            _context.Funcoes.Update(funcao);

            return Task.CompletedTask;
        }

        public async Task<Funcao?> ObterPorIdAsync(Guid id)
        {
            var sql =
           """
                SELECT *
                FROM Funcoes
                WHERE Id = @Id
            """;

            using var connection = Connection();

            return await connection
                .QueryFirstOrDefaultAsync<Funcao>(
                    sql,
                    new { Id = id });
        }

        public async Task<IEnumerable<Funcao>> ObterTodosAsync()
        {
            var sql = """
                SELECT *
                FROM Funcoes
            """;

            using var connection = Connection();

            return await connection.QueryAsync<Funcao>(sql);
        }

        public Task RemoverAsync(Funcao funcao)
        {
            _context.Funcoes.Remove(funcao);

            return Task.CompletedTask;
        }

        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
