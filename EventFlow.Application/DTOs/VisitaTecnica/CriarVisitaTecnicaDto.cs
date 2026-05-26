using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs.VisitaTecnica
{
    public class CriarVisitaTecnicaDto
    {
        public Guid PropostaId { get; set; }
        public DateTime DataAgendada { get; set; }
        public string Responsavel { get; set; }
        public string Observacoes { get; set; }
    }
}
