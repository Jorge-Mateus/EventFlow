using EventFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs.MovimentacaoFinanceira
{
    public class AtualizarMovimentacaoFinanceiraDto
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public TipoMovimentacao Tipo { get; set; }
    }
}
