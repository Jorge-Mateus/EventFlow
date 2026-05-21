using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs.Proposta
{
    public class CriarPropostaDto
    {
        public Guid ClienteId { get; set; }

        public List<CriarPropostaItemDto> Itens { get; set; }
            = new();
    }
}
