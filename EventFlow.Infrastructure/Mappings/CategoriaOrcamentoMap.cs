using EventFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlow.Infrastructure.Mappings;

public class CategoriaOrcamentoMap :
    IEntityTypeConfiguration<CategoriaOrcamento>
{
    public void Configure(
        EntityTypeBuilder<CategoriaOrcamento> builder)
    {
        builder.ToTable("CategoriasOrcamento");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(150);
    }
}