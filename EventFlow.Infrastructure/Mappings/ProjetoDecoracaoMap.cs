using EventFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Infrastructure.Mappings
{
    public class ProjetoDecoracaoMap : IEntityTypeConfiguration<ProjetoDecoracao>
    {
        public void Configure(EntityTypeBuilder<ProjetoDecoracao> builder)
        {
            builder.ToTable("ProjetosDecoracao");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Observacoes)
                .HasMaxLength(1000);

            builder
                .HasOne(x => x.Proposta)
                .WithMany()
                .HasForeignKey(x => x.PropostaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.Arquivos)
                .WithOne(x => x.ProjetoDecoracao)
                .HasForeignKey(x => x.ProjetoDecoracaoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Metadata
                .FindNavigation(nameof(ProjetoDecoracao.Arquivos))
                ?.SetPropertyAccessMode(
                    PropertyAccessMode.Field);
        }
    }
}
