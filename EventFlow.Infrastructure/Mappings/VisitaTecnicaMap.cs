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
    public class VisitaTecnicaMap : IEntityTypeConfiguration<VisitaTecnica>
    {
        public void Configure(EntityTypeBuilder<VisitaTecnica> builder)
        {
            builder.ToTable("VisitasTecnicas");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Responsavel).IsRequired().HasMaxLength(200);

            builder.Property(x => x.Observacoes).HasMaxLength(1000);

            builder.HasOne(x => x.Proposta)
                .WithMany(x => x.VisitasTecnicas)
                .HasForeignKey(x => x.PropostaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
