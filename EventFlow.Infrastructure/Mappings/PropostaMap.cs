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
    public class PropostaMap :
       IEntityTypeConfiguration<Proposta>
    {
        public void Configure(
            EntityTypeBuilder<Proposta> builder)
        {
            builder.ToTable("Propostas");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Ignore(x => x.ValorTotal);

            builder.HasMany<PropostaItem>("_itens")
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}