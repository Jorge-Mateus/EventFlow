using EventFlow.Domain.Enums;

namespace EventFlow.Domain.Entities
{
    public class Proposta : BaseEntity
    {
        private readonly List<PropostaCategoria> _categorias = new();

        public StatusProposta Status { get; private set; }
        public IReadOnlyCollection<PropostaCategoria> Categorias => _categorias.AsReadOnly();

        public decimal ValorTotal { get; private set; }

        public Guid EventoId { get; private set; }

        public Evento Evento { get; private set; }
        public ICollection<VisitaTecnica> VisitasTecnicas { get; private set; } = new List<VisitaTecnica>();
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
        public void SolicitarAjuste()
        {
            Status = StatusProposta.EmAjuste;
        }

        public void AgendarVisitaTecnica()
        {
            Status = StatusProposta.VisitaTecnicaAgendada;
        }

        public void IniciarProjeto3D()
        {
            Status = StatusProposta.EmProjeto3D;
        }

        public void AprovarProjeto()
        {
            Status = StatusProposta.ProjetoAprovado;
        }

        public void IniciarMontagem()
        {
            Status = StatusProposta.EmMontagem;
        }

        public void Finalizar()
        {
            Status = StatusProposta.Finalizada;
        }

        public void Cancelar()
        {
            Status = StatusProposta.Cancelada;
        }
        public void DefinirEvento(Evento evento)
        {
            Evento = evento;
        }

    }
}