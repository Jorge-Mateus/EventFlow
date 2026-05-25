using EventFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlow.Infrastructure.Mappings
{
    public class PropostaCategoriaItemMap : IEntityTypeConfiguration<PropostaCategoriaItem>
    {
        public void Configure(EntityTypeBuilder<PropostaCategoriaItem> builder)
        {
            builder.ToTable(
                "PropostaCategoriaItens");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasMaxLength(300);

            builder
                .HasOne(x => x.PropostaCategoria)
                .WithMany(x => x.Itens)
                .HasForeignKey(
                    x => x.PropostaCategoriaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}