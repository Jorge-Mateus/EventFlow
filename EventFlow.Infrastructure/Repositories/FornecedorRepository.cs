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
    public class FornecedorRepository
    : IFornecedorRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public FornecedorRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private IDbConnection Connection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task AdicionarAsync(Fornecedor fornecedor)
        {
            await _context.Fornecedores
                .AddAsync(fornecedor);
        }

        public Task AtualizarAsync(Fornecedor fornecedor)
        {
            _context.Fornecedores.Update(fornecedor);

            return Task.CompletedTask;
        }

        public async Task<Fornecedor?> ObterPorIdAsync(Guid id)
        {
            var sql =
            @"
                SELECT *
                FROM Fornecedores
                WHERE Id = @Id
            ";

            using var connection = Connection();

            return await connection.QueryFirstOrDefaultAsync<Fornecedor>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Fornecedor>> ObterTodosAsync()
        {
            var sql =
             @"
                SELECT *
                FROM Fornecedores
            ";

            using var connection = Connection();

            return await connection.QueryAsync<Fornecedor>(sql);
        }

        public Task RemoverAsync(Fornecedor fornecedor)
        {
            _context.Fornecedores.Remove(fornecedor);

            return Task.CompletedTask;
        }

        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
