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
    internal class ColaboradorRepository : IColaboradorRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public ColaboradorRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private IDbConnection Connection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task AdicionarAsync(Colaborador colaborador)
        {
            await _context.Colaboradores.AddAsync(colaborador);
        }

        public Task AtualizarAsync(Colaborador colaborador)
        {
            _context.Colaboradores.Update(colaborador);

            return Task.CompletedTask;
        }

        public async Task<Colaborador?> ObterPorIdAsync(Guid id)
        {
            var sql =
            """
                SELECT *
                FROM Colaboradores
                WHERE Id = @Id
            """;

            using var connection = Connection();

            return await connection.QueryFirstOrDefaultAsync<Colaborador>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Colaborador>> ObterTodosAsync()
        {
            var sql =
            """
                SELECT *
                FROM Colaboradores
            """;

            using var connection = Connection();

            return await connection.QueryAsync<Colaborador>(sql);
        }

        public Task RemoverAsync(Colaborador colaborador)
        {
            _context.Colaboradores.Remove(colaborador);

            return Task.CompletedTask;
        }

        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
