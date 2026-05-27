using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs.EquipeEvento
{
    public class CriarEquipeEventoDto
    {
        public Guid EventoId { get; set; }
        public Guid ColaboradorId { get; set; }
        public decimal ValorPagamento { get; set; }
    }
}
