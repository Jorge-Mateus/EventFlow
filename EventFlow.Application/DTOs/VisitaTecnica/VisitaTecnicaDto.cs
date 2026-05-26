using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs.VisitaTecnica
{
    public class VisitaTecnicaDto
    {
        public Guid Id { get; set; }
        public Guid PropostaId { get; set; }
        public DateTime DataAgendada { set; get; }
        public string Responsavel { get; set; }
        public string Observacoes { get; set; }
    }
}
