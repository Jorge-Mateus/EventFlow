using EventFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Domain.Entities
{
    public class Proposta : BaseEntity
    {
        private readonly List<PropostaItem> _itens = new();

        public Guid ClienteId { get; private set; }

        public StatusProposta Status { get; private set; }

        public decimal ValorTotal =>
            _itens.Sum(x => x.Total);

        public IReadOnlyCollection<PropostaItem> Itens =>
            _itens;

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
            _itens.Add(new PropostaItem(
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
