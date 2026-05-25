using EventFlow.Domain.Enums;

namespace EventFlow.Domain.Entities
{
    public class Proposta : BaseEntity
    {
        private readonly List<PropostaCategoria> _categorias = new();

        public StatusProposta Status { get; private set; }
        public IReadOnlyCollection<PropostaCategoria> Categorias => _categorias.AsReadOnly();

        public decimal ValorTotal => _categorias.Sum(x => x.Valor);

        public Guid EventoId { get; private set; }

        public Evento Evento { get; private set; }

        protected Proposta() { }

        public Proposta(Guid eventoId)
        {
            EventoId = eventoId;
            Status = StatusProposta.Rascunho;
        }

        public void CarregarCategorias(IEnumerable<PropostaCategoria> categorias)
        {
            _categorias.Clear();
            _categorias.AddRange(categorias);
        }

        public void AdicionarCategoria(Guid categoriaOrcamentoId, decimal valor)
        {
            _categorias.Add(new PropostaCategoria(categoriaOrcamentoId, valor));
        }

        public void Atualizar(Guid eventoId, StatusProposta status)
        {
            EventoId = eventoId;
            Status = status;
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