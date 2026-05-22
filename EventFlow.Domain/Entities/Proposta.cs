using EventFlow.Domain.Enums;

namespace EventFlow.Domain.Entities
{
    public class Proposta : BaseEntity
    {
        public StatusProposta Status { get; private set; }

        public ICollection<PropostaItem> Itens { get; private set; }
            = new List<PropostaItem>();
        public decimal ValorTotal =>
            Itens.Sum(x => x.Total);

        public Guid EventoId { get; private set; }

        public Evento Evento { get; private set; }

        protected Proposta() { }

        public Proposta(Guid eventoId)
        {
            EventoId = eventoId;
            Status = StatusProposta.Rascunho;
        }

        public void AdicionarItem(
            Guid categoriaOrcamentoId,
            string descricao,
            int quantidade,
            decimal valorUnitario)
        {
            Itens.Add(new PropostaItem(
                categoriaOrcamentoId,
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