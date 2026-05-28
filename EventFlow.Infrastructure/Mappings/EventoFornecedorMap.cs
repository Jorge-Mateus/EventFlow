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
    public class EventoFornecedorMap : IEntityTypeConfiguration<EventoFornecedor>
    {
        public void Configure(EntityTypeBuilder<EventoFornecedor> builder)
        {
            builder.ToTable("EventosFornecedores");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ValorContratado)
                .HasColumnType("decimal(18,2)");

            builder
                .HasOne(x => x.Evento)
                .WithMany(x => x.Fornecedores)
                .HasForeignKey(x => x.EventoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Fornecedor)
                .WithMany(x => x.Eventos)
                .HasForeignKey(x => x.FornecedorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
