using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs.Contrato
{
    public class CriarContratoDto
    {
        public Guid EventoId { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public string? CaminhoArquivo { get; set; }
        public string? TipoArquivo { get; set; }
    }
}
