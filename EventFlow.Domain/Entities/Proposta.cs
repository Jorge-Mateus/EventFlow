using EventFlow.Domain.Enums;

namespace EventFlow.Domain.Entities
{
    public class Proposta : BaseEntity
    {
        private readonly List<PropostaItem> _itens
            = new();

        public StatusProposta Status { get; private set; }

        public IReadOnlyCollection<PropostaItem> Itens => _itens.AsReadOnly();

        public decimal ValorTotal =>
            _itens.Sum(x => x.Total);

        public Guid EventoId { get; private set; }

        public Evento Evento { get; private set; }

        protected Proposta() { }

        public Proposta(Guid eventoId)
        {
            EventoId = eventoId;
            Status = StatusProposta.Rascunho;
        }
        public void CarregarItens(IEnumerable<PropostaItem> itens)
        {
            _itens.Clear();
            _itens.AddRange(itens);
        }
        public void AdicionarItem(Guid categoriaOrcamentoId, string descricao, int quantidade, decimal valorUnitario)
        {
            _itens.Add(new PropostaItem(
                categoriaOrcamentoId,
                descricao,
                quantidade,
                valorUnitario));
        }

        public void Enviar()
        {
            Status = StatusProposta.Enviada;
        }
        public void Atualizar(Guid eventoId, StatusProposta status)
        {
            EventoId = eventoId;
            Status = status;
        }
        public void Aprovar()
        {
            Status = StatusProposta.Aprovada;
        }
    }
}