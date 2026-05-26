using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Domain.Entities
{
    public class VisitaTecnica : BaseEntity
    {
        public Guid PropostaId { get; private set; }

        public Proposta Proposta { get; private set; }

        public DateTime DataAgendada { get; private set; }

        public string Responsavel { get; private set; }

        public string Observacoes { get; private set; }

        protected VisitaTecnica() { }

        public VisitaTecnica(Guid propostaId, DateTime dataAgendada, string responsavel, string observacoes)
        {
            PropostaId = propostaId;
            DataAgendada = dataAgendada;
            Responsavel = responsavel;
            Observacoes = observacoes;
        }

        public void Atualizar(DateTime dataAgendada, string responsavel, string observacoes)
        {
            DataAgendada = dataAgendada;
            Responsavel = responsavel;
            Observacoes = observacoes;
        }
    }
}
