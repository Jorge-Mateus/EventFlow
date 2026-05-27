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
    public class EquipeEventoMap : IEntityTypeConfiguration<EquipeEvento>
    {
        public void Configure(EntityTypeBuilder<EquipeEvento> builder)
        {
            builder.ToTable("EquipesEvento");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ValorPagamento)
                .HasColumnType("decimal(18,2)");

            builder
                .HasOne(x => x.Evento)
                .WithMany()
                .HasForeignKey(x => x.EventoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Colaborador)
                .WithMany()
                .HasForeignKey(x => x.ColaboradorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
