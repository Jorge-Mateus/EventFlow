using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs.Proposta
{
    public class PropostaCategoriaDetalheDto
    {
        public Guid Id { get; set; }

        public Guid CategoriaOrcamentoId { get; set; }

        public decimal Valor { get; set; }

        public List<PropostaCategoriaItemDto> Itens { get; set; } = new();
    }
}
