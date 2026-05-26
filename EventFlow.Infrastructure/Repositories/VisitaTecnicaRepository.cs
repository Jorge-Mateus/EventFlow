using Dapper;
using EventFlow.Domain.Entities;
using EventFlow.Domain.Interfaces;
using EventFlow.Infrastructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EventFlow.Infrastructure.Repositories
{
    public class VisitaTecnicaRepository : IVisitaTecnicaRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public VisitaTecnicaRepository(
            AppDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private IDbConnection Connection()
        {
            return new SqlConnection(
                _configuration.GetConnectionString(
                    "DefaultConnection"));
        }

        public async Task AdicionarAsync(VisitaTecnica visita)
        {
            await _context.VisitasTecnicas.AddAsync(visita);
        }

        public Task AtualizarAsync(VisitaTecnica visita)
        {
            _context.VisitasTecnicas.Update(visita);

            return Task.CompletedTask;
        }

        public async Task<VisitaTecnica?> ObterPorIdAsync(Guid id)
        {
            var sql = @"
            SELECT *
            FROM VisitasTecnicas
            WHERE Id = @Id
        ";

            using var connection = Connection();

            return await connection.QueryFirstOrDefaultAsync<VisitaTecnica>(sql, new { Id = id });
        }

        public async Task<IEnumerable<VisitaTecnica>> ObterTodosAsync()
        {
            var sql = @"
            SELECT *
            FROM VisitasTecnicas
        ";

            using var connection = Connection();

            return await connection.QueryAsync<VisitaTecnica>(sql);
        }

        public async Task RemoverAsync(VisitaTecnica visita)
        {
            _context.VisitasTecnicas.Remove(visita);

            await _context.SaveChangesAsync();
        }

        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
