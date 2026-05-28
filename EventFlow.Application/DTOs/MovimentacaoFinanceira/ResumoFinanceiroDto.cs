using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs.MovimentacaoFinanceira
{
    public class ResumoFinanceiroDto
    {
        public decimal TotalEntradas { get; set; }
        public decimal TotalSaidas { get; set; }
        public decimal LucroLiquido { get; set; }
    }
}
