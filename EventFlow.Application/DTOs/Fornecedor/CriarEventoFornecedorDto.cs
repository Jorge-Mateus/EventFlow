using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs.Fornecedor
{
    public class CriarEventoFornecedorDto
    {
        public Guid EventoId { get; set; }
        public List<CriarEventoFornecedorItemDto> Itens { get; set; } = new();
    }
}
