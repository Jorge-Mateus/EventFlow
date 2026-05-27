using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs.EquipeEvento
{
    public class EquipeEventoDto
    {
        public Guid Id { get; set; }

        public Guid EventoId { get; set; }

        public Guid ColaboradorId { get; set; }

        public decimal ValorPagamento { get; set; }
    }
}
