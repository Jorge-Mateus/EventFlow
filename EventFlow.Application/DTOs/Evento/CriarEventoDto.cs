

namespace EventFlow.Application.DTOs.Evento
{
    public class CriarEventoDto
    {
        public Guid ClienteId { get; set; }

        public string Nome { get; set; }

        public DateTime DataEvento { get; set; }

        public string LocalEvento { get; set; }

        public int QuantidadeConvidados
        { get; set; }
    }
}