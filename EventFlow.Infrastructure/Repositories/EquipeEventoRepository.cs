using Dapper;
using EventFlow.Domain.Entities;
using EventFlow.Domain.Interfaces;
using EventFlow.Infrastructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EventFlow.Infrastructure.Repositories
{
    public class EquipeEventoRepository : IEquipeEventoRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public EquipeEventoRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private IDbConnection Connection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task AdicionarAsync(EquipeEvento equipeEvento)
        {
            await _context.EquipesEvento.AddAsync(equipeEvento);
        }

        public Task AtualizarAsync(EquipeEvento equipeEvento)
        {
            _context.EquipesEvento.Update(equipeEvento);

            return Task.CompletedTask;
        }

        public async Task<EquipeEvento?> ObterPorIdAsync(Guid id)
        {
            var sql =
            """
                SELECT *
                FROM EquipesEvento
                WHERE Id = @Id
            """;

            using var connection = Connection();

            return await connection.QueryFirstOrDefaultAsync<EquipeEvento>(sql, new { Id = id });
        }

        public async Task<IEnumerable<EquipeEvento>> ObterTodosAsync()
        {
            var sql =
            """
                SELECT *
                FROM EquipesEvento
            """;

            using var connection = Connection();

            return await connection.QueryAsync<EquipeEvento>(sql);
        }

        public async Task<IEnumerable<EquipeEvento>> ObterPorEventoAsync(Guid eventoId)
        {
            var sql =
             """
                SELECT *
                FROM EquipesEvento
                WHERE EventoId = @EventoId
            """;

            using var connection = Connection();

            return await connection.QueryAsync<EquipeEvento>(sql, new { EventoId = eventoId });
        }

        public Task RemoverAsync(EquipeEvento equipeEvento)
        {
            _context.EquipesEvento.Remove(equipeEvento);

            return Task.CompletedTask;
        }

        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
