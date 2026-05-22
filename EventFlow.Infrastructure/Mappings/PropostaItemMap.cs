using EventFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlow.Infrastructure.Mappings;

public class PropostaItemMap :
    IEntityTypeConfiguration<PropostaItem>
{
    public void Configure(
        EntityTypeBuilder<PropostaItem> builder)
    {
        builder.ToTable("PropostaItens");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.Quantidade)
            .IsRequired();

        builder.Property(x => x.ValorUnitario)
            .HasColumnType("decimal(18,2)");
        builder
        .HasOne(x => x.CategoriaOrcamento)
        .WithMany(x => x.Itens)
        .HasForeignKey(x => x.CategoriaOrcamentoId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}