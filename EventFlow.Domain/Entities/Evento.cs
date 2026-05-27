using System.ComponentModel.DataAnnotations.Schema;

namespace EventFlow.Domain.Entities
{
    public class Evento : BaseEntity
    {
        public Guid ClienteId { get; private set; }

        public Cliente Cliente { get; private set; }

        public string Nome { get; private set; }

        public DateTime DataEvento { get; private set; }

        public string LocalEvento { get; private set; }

        public int QuantidadeConvidados { get; private set; }

        public ICollection<Proposta> Propostas { get; private set; } = new List<Proposta>();
        [NotMapped]
        public bool TemEquipe { get; private set; }
        protected Evento() { }

        public Evento(Guid clienteId, string nome, DateTime dataEvento, string localEvento, int quantidadeConvidados)
        {
            ClienteId = clienteId;
            Nome = nome;
            DataEvento = dataEvento;
            LocalEvento = localEvento;
            QuantidadeConvidados = quantidadeConvidados;
        }
        public void Atualizar(Guid clienteId, string nome, DateTime dataEvento, string localEvento, int quantidadeConvidados)
        {
            ClienteId = clienteId;
            Nome = nome;
            DataEvento = dataEvento;
            LocalEvento = localEvento;
            QuantidadeConvidados = quantidadeConvidados;
        }
    }
}
