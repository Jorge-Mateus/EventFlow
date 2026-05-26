using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs.VisitaTecnica
{
    public class AtualizarVisitaTecnicaDto
    {
        public Guid Id { get; set; }
        public DateTime DataAgendada { get; set; }
        public string Responsavel { get; set; }
        public string Observacoes { get; set; }
    }
}
