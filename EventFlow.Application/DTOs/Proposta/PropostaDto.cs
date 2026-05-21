using EventFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs.Proposta
{
    public class PropostaDto
    {
        public Guid Id { get; set; }

        public Guid ClienteId { get; set; }

        public StatusProposta Status { get; set; }

        public decimal ValorTotal { get; set; }

        public List<PropostaItemDto> Itens { get; set; }
            = new();
    }
}
