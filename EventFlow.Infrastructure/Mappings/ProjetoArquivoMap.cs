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
    public class ProjetoArquivoMap : IEntityTypeConfiguration<ProjetoArquivo>
    {
        public void Configure(EntityTypeBuilder<ProjetoArquivo> builder)
        {
            builder.ToTable("ProjetoArquivos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.NomeArquivo)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(x => x.Caminho)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.Tipo)
                .HasMaxLength(50);

            builder
                .HasOne(x => x.ProjetoDecoracao)
                .WithMany(x => x.Arquivos)
                .HasForeignKey(x => x.ProjetoDecoracaoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
