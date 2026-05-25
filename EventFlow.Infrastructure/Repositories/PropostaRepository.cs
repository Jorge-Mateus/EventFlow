using Dapper;
using EventFlow.Domain.Entities;
using EventFlow.Domain.Interfaces;
using EventFlow.Infrastructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EventFlow.Infrastructure.Repositories;

public class PropostaRepository : IPropostaRepository
{
    private readonly AppDbContext _context;

    private readonly IConfiguration _configuration;

    public PropostaRepository(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    private IDbConnection Connection()
    {
        return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    }

    public async Task AdicionarAsync(Proposta proposta)
    {
        await _context.Propostas.AddAsync(
            proposta);
    }

    public Task AtualizarAsync(Proposta proposta)
    {
        _context.Propostas.Update(proposta);

        return Task.CompletedTask;
    }

    public async Task<Proposta?> ObterPorIdAsync(
     Guid id)
    {
        var sql = @"
            SELECT
                p.*,
                ISNULL(SUM(pc.Valor), 0) AS ValorTotal
            FROM Propostas p
            LEFT JOIN PropostaCategorias pc
                ON pc.PropostaId = p.Id
            WHERE p.Id = @Id
            GROUP BY
                p.Id,
                p.EventoId,
                p.Status,
                p.CreatedAt,
                p.UpdatedAt,
                p.Active;

            SELECT *
            FROM PropostaCategorias
            WHERE PropostaId = @Id;

            SELECT pci.*
            FROM PropostaCategoriaItens pci
            INNER JOIN PropostaCategorias pc
                ON pc.Id = pci.PropostaCategoriaId
            WHERE pc.PropostaId = @Id;
        ";

        using var connection = Connection();

        using var multi =
            await connection.QueryMultipleAsync(
                sql,
                new { Id = id });

        var proposta =
            await multi.ReadFirstOrDefaultAsync<Proposta>();

        if (proposta is null)
            return null;

        var categorias =
            (await multi.ReadAsync<PropostaCategoria>())
            .ToList();

        var itens =
            (await multi.ReadAsync<PropostaCategoriaItem>())
            .ToList();

        foreach (var categoria in categorias)
        {
            categoria.CarregarItens(
                itens.Where(x =>
                    x.PropostaCategoriaId ==
                    categoria.Id));
        }

        proposta.CarregarCategorias(
            categorias);

        return proposta;
    }

    public async Task<IEnumerable<Proposta>> ObterTodosAsync()
    {
        var sql = @"
            SELECT
                p.*,
                ISNULL(SUM(pc.Valor), 0) AS ValorTotal
            FROM Propostas p
            LEFT JOIN PropostaCategorias pc ON pc.PropostaId = p.Id
            GROUP BY
                    p.Id,
                    p.EventoId,
                    p.Status,
                    p.CreatedAt,
                    p.UpdatedAt,
                    p.Active
        ";

        using var connection = Connection();

        return await connection.QueryAsync<Proposta>(sql);
    }

    public async Task SalvarAlteracoesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Proposta proposta)
    {
        _context.Propostas.Remove(proposta);

        await _context.SaveChangesAsync();
    }
}