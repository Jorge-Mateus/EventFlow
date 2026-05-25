using EventFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlow.Infrastructure.Mappings
{
    public class PropostaCategoriaMap : IEntityTypeConfiguration<PropostaCategoria>
    {
        public void Configure(EntityTypeBuilder<PropostaCategoria> builder)
        {
            builder.ToTable("PropostaCategorias");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Valor)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder
                .HasOne(x => x.Proposta)
                .WithMany(x => x.Categorias)
                .HasForeignKey(x => x.PropostaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.CategoriaOrcamento)
                .WithMany()
                .HasForeignKey(x => x.CategoriaOrcamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Metadata
                .FindNavigation(
                    nameof(PropostaCategoria.Itens))
                ?.SetPropertyAccessMode(
                    PropertyAccessMode.Field);
        }
    }
}