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
    namespace EventFlow.Infrastructure.Mappings
    {
        public class ContratoMap : IEntityTypeConfiguration<Contrato>
        {
            public void Configure(EntityTypeBuilder<Contrato> builder)
            {
                builder.ToTable("Contratos");

                builder.HasKey(x => x.Id);

                builder.Property(x => x.Titulo)
                    .IsRequired()
                    .HasMaxLength(200);

                builder.Property(x => x.Descricao)
                    .HasMaxLength(500);

                builder.Property(x => x.CaminhoArquivo)
                    .IsRequired();

                builder.Property(x => x.TipoArquivo)
                    .HasMaxLength(100);

                builder.HasOne(x => x.Evento)
                    .WithMany(x => x.Contratos)
                    .HasForeignKey(x => x.EventoId)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }
    }
}
