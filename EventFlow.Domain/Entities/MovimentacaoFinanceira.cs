using EventFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Domain.Entities
{
    public class MovimentacaoFinanceira : BaseEntity
    {
        public Guid EventoId { get; private set; }

        public Evento Evento { get; private set; }

        public string Descricao { get; private set; }

        public decimal Valor { get; private set; }

        public DateTime DataMovimentacao { get; private set; }

        public TipoMovimentacao Tipo { get; private set; }

        protected MovimentacaoFinanceira() { }

        public MovimentacaoFinanceira(Guid eventoId, string descricao, decimal valor, TipoMovimentacao tipo)
        {
            EventoId = eventoId;
            Descricao = descricao;
            Valor = valor;
            Tipo = tipo;
            DataMovimentacao = DateTime.Now;
        }

        public void Atualizar(
            string descricao,
            decimal valor,
            TipoMovimentacao tipo)
        {
            Descricao = descricao;
            Valor = valor;
            Tipo = tipo;
        }
    }
}
