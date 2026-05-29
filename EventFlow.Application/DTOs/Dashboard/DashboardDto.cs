using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs.Dashboard
{
    public class DashboardDto
    {
        public int TotalEventos { get; set; }
        public int EventosFuturos { get; set; }
        public int EventosMes { get; set; }
        public decimal ReceitaTotal { get; set; }
        public decimal DespesaTotal { get; set; }
        public int EventosSemEquipe { get; set; }
        public int EventosSemFornecedor { get; set; }
        public decimal LucroTotal => ReceitaTotal - DespesaTotal;
        public List<DashboardEventoDto> ProximosEventos { get; set; } = new();
        public List<DashboardFinanceiroMensalDto> FinanceiroMensal { get; set; } = new();
    }
}
