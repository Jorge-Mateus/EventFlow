using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs.Proposta
{
    public class AtualizarPropostaDto
    {
        public Guid Id { get; set; }
        public Guid EventoId { get; set; }
        public int Status { get; set; }
    }
}
