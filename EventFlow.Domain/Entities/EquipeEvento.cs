using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Domain.Entities
{

    public class EquipeEvento : BaseEntity
    {
        public Guid EventoId { get; private set; }

        public Evento Evento { get; private set; }

        public Guid ColaboradorId { get; private set; }

        public Colaborador Colaborador { get; private set; }

        public decimal ValorPagamento { get; private set; }

        protected EquipeEvento() { }

        public EquipeEvento(Guid eventoId, Guid colaboradorId, decimal valorPagamento)
        {
            EventoId = eventoId;
            ColaboradorId = colaboradorId;
            ValorPagamento = valorPagamento;
        }

        public void Atualizar(Guid colaboradorId, decimal valorPagamento)
        {
            ColaboradorId = colaboradorId;
            ValorPagamento = valorPagamento;
        }
    }
}
