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
    public class PropostaItemMap :
     IEntityTypeConfiguration<PropostaItem>
    {
        public void Configure(
            EntityTypeBuilder<PropostaItem> builder)
        {
            builder.ToTable("PropostaItens");

            builder.HasKey("Id");

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(x => x.Quantidade)
                .IsRequired();

            builder.Property(x => x.ValorUnitario)
                .HasColumnType("decimal(18,2)");
        }
    }
}
