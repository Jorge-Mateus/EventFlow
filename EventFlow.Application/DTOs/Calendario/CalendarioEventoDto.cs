using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs.Calendario
{
    public class CalendarioEventoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataEvento { get; set; }
        public string LocalEvento { get; set; }
        public bool TemEquipe { get; set; }
        public bool TemFornecedor { get; set; }
    }
}
