using EventFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventFlow.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Evento> Eventos { get; set; }
    public DbSet<Proposta> Propostas { get; set; }
    public DbSet<PropostaCategoria> PropostaCategorias { get; set; }
    public DbSet<PropostaCategoriaItem> PropostaCategoriaItens { get; set; }
    public DbSet<VisitaTecnica> VisitasTecnicas { get; set; }
    public DbSet<CategoriaOrcamento> CategoriasOrcamento { get; set; }
    public DbSet<ProjetoDecoracao> ProjetosDecoracao { get; set; }
    public DbSet<ProjetoArquivo> ProjetoArquivos { get; set; }
    public DbSet<Colaborador> Colaboradores { get; set; }
    public DbSet<Funcao> Funcoes { get; set; }
    public DbSet<EquipeEvento> EquipesEvento { get; set; }
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}