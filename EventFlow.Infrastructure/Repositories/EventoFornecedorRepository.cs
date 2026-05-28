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
    public class EventoFornecedorRepository : IEventoFornecedorRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public EventoFornecedorRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private IDbConnection Connection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task AdicionarAsync(EventoFornecedor fornecedor)
        {
            await _context.EventosFornecedores.AddAsync(fornecedor);
        }

        public async Task<IEnumerable<EventoFornecedor>> ObterPorEventoAsync(Guid eventoId)
        {
            var sql =
             @"
                SELECT *
                FROM EventosFornecedores
                WHERE EventoId = @EventoId
            ";

            using var connection = Connection();

            return await connection.QueryAsync<EventoFornecedor>(sql, new { EventoId = eventoId });
        }

        public Task RemoverAsync(EventoFornecedor fornecedor)
        {
            _context.EventosFornecedores.Remove(fornecedor);

            return Task.CompletedTask;
        }

        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
