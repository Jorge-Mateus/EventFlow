using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs.Fornecedor
{
    public class CriarEventoFornecedorItemDto
    {
        public Guid FornecedorId { get; set; }
        public decimal ValorContratado { get; set; }
    }
}
