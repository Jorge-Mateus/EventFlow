using EventFlow.Domain.Enums;

namespace EventFlow.Domain.Entities
{
    public class Proposta : BaseEntity
    {
        public Guid ClienteId { get; private set; }

        public StatusProposta Status { get; private set; }

        public ICollection<PropostaItem> Itens { get; private set; }
            = new List<PropostaItem>();
        public Cliente Cliente { get; private set; }
        public decimal ValorTotal =>
            Itens.Sum(x => x.Total);

        protected Proposta() { }

        public Proposta(Guid clienteId)
        {
            ClienteId = clienteId;
            Status = StatusProposta.Rascunho;
        }

        public void AdicionarItem(
            string descricao,
            int quantidade,
            decimal valorUnitario)
        {
            Itens.Add(new PropostaItem(
                descricao,
                quantidade,
                valorUnitario));
        }

        public void Enviar()
        {
            Status = StatusProposta.Enviada;
        }

        public void Aprovar()
        {
            Status = StatusProposta.Aprovada;
        }
    }
}