using Dapper;
using EventFlow.Domain.Entities;
using EventFlow.Domain.Interfaces;
using EventFlow.Infrastructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EventFlow.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    private readonly IConfiguration _configuration;

    public ClienteRepository(AppDbContext context, IConfiguration configuration)
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

    public async Task AdicionarAsync(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
    }

    public Task AtualizarAsync(Cliente cliente)
    {
        _context.Clientes.Update(cliente);

        return Task.CompletedTask;
    }

    public async Task<Cliente?> ObterPorIdAsync(Guid id)
    {
        var sql = @"
            SELECT
                Id,
                Nome,
                Telefone,
                Email,
                Active,
                CreatedAt,
                UpdatedAt
            FROM Clientes
            WHERE Id = @Id
        ";

        using var connection = Connection();

        return await connection
            .QueryFirstOrDefaultAsync<Cliente>(
                sql,
                new { Id = id });
    }

    public async Task<IEnumerable<Cliente>> ObterTodosAsync()
    {
        var sql = @"
            SELECT
                Id,
                Nome,
                Telefone,
                Email,
                Active,
                CreatedAt,
                UpdatedAt
            FROM Clientes
            WHERE Active = 1
        ";

        using var connection = Connection();

        return await connection
            .QueryAsync<Cliente>(sql);
    }

    public async Task SalvarAlteracoesAsync()
    {
        await _context.SaveChangesAsync();
    }
    public Task RemoverAsync(Cliente cliente)
    {
        _context.Clientes.Remove(cliente);

        return Task.CompletedTask;
    }
}