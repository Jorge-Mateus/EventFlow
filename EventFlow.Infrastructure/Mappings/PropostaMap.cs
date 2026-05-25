using EventFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlow.Infrastructure.Mappings;

public class PropostaMap : IEntityTypeConfiguration<Proposta>
{
    public void Configure(EntityTypeBuilder<Proposta> builder)
    {
        builder.ToTable("Propostas");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Ignore(x => x.ValorTotal);

        builder
            .HasMany(x => x.Categorias)
            .WithOne(x => x.Proposta)
            .HasForeignKey(x => x.PropostaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata
            .FindNavigation(
                nameof(Proposta.Categorias))
            ?.SetPropertyAccessMode(
                PropertyAccessMode.Field);

        builder
            .HasOne(x => x.Evento)
            .WithMany(x => x.Propostas)
            .HasForeignKey(x => x.EventoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}