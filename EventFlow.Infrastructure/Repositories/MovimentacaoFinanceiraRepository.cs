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
    public class MovimentacaoFinanceiraRepository : IMovimentacaoFinanceiraRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public MovimentacaoFinanceiraRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private IDbConnection Connection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task AdicionarAsync(MovimentacaoFinanceira movimentacao)
        {
            await _context.MovimentacoesFinanceiras.AddAsync(movimentacao);
        }

        public Task AtualizarAsync(MovimentacaoFinanceira movimentacao)
        {
            _context.MovimentacoesFinanceiras.Update(movimentacao);

            return Task.CompletedTask;
        }

        public async Task<MovimentacaoFinanceira?>
            ObterPorIdAsync(Guid id)
        {
            var sql =
             @"
                SELECT *
                FROM MovimentacoesFinanceiras
                WHERE Id = @Id
             ";

            using var connection = Connection();

            return await connection.QueryFirstOrDefaultAsync<MovimentacaoFinanceira>(sql, new { Id = id });
        }

        public async Task<IEnumerable<MovimentacaoFinanceira>> ObterPorEventoAsync(Guid eventoId)
        {
            var sql =
             @"
                SELECT *
                FROM MovimentacoesFinanceiras
                WHERE EventoId = @EventoId
             ";

            using var connection = Connection();

            return await connection.QueryAsync<MovimentacaoFinanceira>(sql, new { EventoId = eventoId });
        }

        public Task RemoverAsync(MovimentacaoFinanceira movimentacao)
        {
            _context.MovimentacoesFinanceiras.Remove(movimentacao);
            return Task.CompletedTask;
        }

        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MovimentacaoFinanceira>> ObterTodosAsync()
        {
            var sql =
            @"
               SELECT *
               FROM MovimentacoesFinanceiras
            ";
            using var connection = Connection();
            return await connection.QueryAsync<MovimentacaoFinanceira>(sql);
        }
    }
}
