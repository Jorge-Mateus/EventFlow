using EventFlow.Application.DTOs.MovimentacaoFinanceira;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs.Evento
{
    public class EventoDetalheDto
    {
        public Guid Id { get; set; }

        public Guid ClienteId { get; set; }

        public string Nome { get; set; }

        public DateTime DataEvento { get; set; }

        public string LocalEvento { get; set; }
        public bool TemEquipe { get; set; }
        public bool TemFornecedor { get; set; }
        public int QuantidadeConvidados { get; set; }
        public decimal TotalEntradas { get; set; }

        public decimal TotalSaidas { get; set; }

        public decimal LucroLiquido { get; set; }
        public List<MovimentacaoFinanceiraDto> Movimentacoes { get; set; } = new();
    }
}
