using EventFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs.MovimentacaoFinanceira
{
    public class MovimentacaoFinanceiraDto
    {
        public Guid Id { get; set; }
        public Guid EventoId { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataMovimentacao { get; set; }
        public TipoMovimentacao Tipo { get; set; }
    }
}
