using EventFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlow.Infrastructure.Mappings;

public class EventoMap :
    IEntityTypeConfiguration<Evento>
{
    public void Configure(
        EntityTypeBuilder<Evento> builder)
    {
        builder.ToTable("Eventos");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.LocalEvento)
            .HasMaxLength(300);

        builder.Property(x => x.QuantidadeConvidados)
            .IsRequired();

        builder.Property(x => x.DataEvento)
            .IsRequired();

        builder
            .HasOne(x => x.Cliente)
            .WithMany(x => x.Eventos)
            .HasForeignKey(x => x.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}