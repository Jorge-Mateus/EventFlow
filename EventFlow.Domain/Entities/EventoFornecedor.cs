using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Domain.Entities
{
    public class EventoFornecedor : BaseEntity
    {
        public Guid EventoId { get; private set; }

        public Evento Evento { get; private set; }

        public Guid FornecedorId { get; private set; }

        public Fornecedor Fornecedor { get; private set; }

        public decimal ValorContratado { get; private set; }

        protected EventoFornecedor() { }

        public EventoFornecedor(Guid eventoId, Guid fornecedorId, decimal valorContratado)
        {
            EventoId = eventoId;
            FornecedorId = fornecedorId;
            ValorContratado = valorContratado;
        }

        public void Atualizar(decimal valorContratado)
        {
            ValorContratado = valorContratado;
        }
    }
}
