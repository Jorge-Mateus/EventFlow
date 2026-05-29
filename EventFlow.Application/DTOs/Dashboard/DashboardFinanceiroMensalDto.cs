using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs.Dashboard
{
    public class DashboardFinanceiroMensalDto
    {
        public string Mes { get; set; }

        public decimal Entradas { get; set; }

        public decimal Saidas { get; set; }

        public decimal Lucro =>
            Entradas - Saidas;
    }

}
