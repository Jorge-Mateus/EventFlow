using Dapper;
using EventFlow.Domain.Entities;
using EventFlow.Domain.Interfaces;
using EventFlow.Infrastructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EventFlow.Infrastructure.Repositories;

public class EventoRepository : IEventoRepository
{
    private readonly AppDbContext _context;

    private readonly IConfiguration _configuration;

    public EventoRepository(
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
        Evento evento)
    {
        await _context.Eventos.AddAsync(evento);
    }

    public Task AtualizarAsync(
        Evento evento)
    {
        _context.Eventos.Update(evento);

        return Task.CompletedTask;
    }

    public async Task<Evento?> ObterPorIdAsync(Guid id)
    {
        var sql = @"
        SELECT
            e.*,
            CASE
                WHEN EXISTS (
                    SELECT 1
                    FROM EquipesEvento ee
                    WHERE ee.EventoId = e.Id
                )
                THEN CAST(1 AS BIT)
                ELSE CAST(0 AS BIT)
            END AS TemEquipe
        FROM Eventos e
        WHERE e.Id = @Id
    ";

        using var connection = Connection();

        return await connection.QueryFirstOrDefaultAsync<Evento>(sql, new { Id = id });
    }

    public async Task<IEnumerable<Evento>> ObterTodosAsync()
    {
        var sql = @"
        SELECT
            e.*,
            CASE
                WHEN EXISTS (
                    SELECT 1
                    FROM EquipesEvento ee
                    WHERE ee.EventoId = e.Id
                )
                THEN CAST(1 AS BIT)
                ELSE CAST(0 AS BIT)
            END AS TemEquipe
        FROM Eventos e
    ";

        using var connection = Connection();

        return await connection.QueryAsync<Evento>(sql);
    }

    public async Task SalvarAlteracoesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public Task RemoverAsync(Evento evento)
    {
        _context.Eventos.Remove(evento);
        return Task.CompletedTask;
    }
}