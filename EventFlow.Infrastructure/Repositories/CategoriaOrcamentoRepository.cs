using Dapper;
using EventFlow.Domain.Entities;
using EventFlow.Domain.Interfaces;
using EventFlow.Infrastructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EventFlow.Infrastructure.Repositories;

public class CategoriaOrcamentoRepository
    : ICategoriaOrcamentoRepository
{
    private readonly AppDbContext _context;

    private readonly IConfiguration _configuration;

    public CategoriaOrcamentoRepository(
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

    public async Task AdicionarAsync(
        CategoriaOrcamento categoria)
    {
        await _context
            .CategoriasOrcamento
            .AddAsync(categoria);
    }

    public Task AtualizarAsync(
        CategoriaOrcamento categoria)
    {
        _context
            .CategoriasOrcamento
            .Update(categoria);

        return Task.CompletedTask;
    }

    public async Task<CategoriaOrcamento?>
        ObterPorIdAsync(Guid id)
    {
        var sql = @"
            SELECT
                *
            FROM CategoriasOrcamento
            WHERE Id = @Id
        ";

        using var connection = Connection();

        return await connection
            .QueryFirstOrDefaultAsync<
                CategoriaOrcamento>(
                    sql,
                    new { Id = id });
    }

    public async Task<IEnumerable<CategoriaOrcamento>>
        ObterTodosAsync()
    {
        var sql = @"
            SELECT
                *
            FROM CategoriasOrcamento
        ";

        using var connection = Connection();

        return await connection
            .QueryAsync<CategoriaOrcamento>(
                sql);
    }

    public async Task SalvarAlteracoesAsync()
    {
        await _context.SaveChangesAsync();
    }
}