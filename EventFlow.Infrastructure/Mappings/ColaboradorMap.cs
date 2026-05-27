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
    public class ColaboradorMap : IEntityTypeConfiguration<Colaborador>
    {
        public void Configure(EntityTypeBuilder<Colaborador> builder)
        {
            builder.ToTable("Colaboradores");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.CPF) 
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Telefone)
                .HasMaxLength(20);

            builder.Property(x => x.Pix)
                .HasMaxLength(100);

            builder.Property(x => x.Ativo)
                .IsRequired();

            builder
                .HasOne(x => x.Funcao)
                .WithMany()
                .HasForeignKey(x => x.FuncaoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
