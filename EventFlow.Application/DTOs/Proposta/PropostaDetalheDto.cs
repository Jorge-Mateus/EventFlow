using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs.Proposta
{
    public class PropostaDetalheDto
    {
        public Guid Id { get; set; }

        public string Status { get; set; }

        public decimal ValorTotal { get; set; }

        public List<PropostaItemDto> Itens
        { get; set; } = new();
    }
}
