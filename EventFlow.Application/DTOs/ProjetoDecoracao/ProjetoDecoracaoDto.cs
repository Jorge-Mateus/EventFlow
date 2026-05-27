using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs.ProjetoDecoracao
{
    public class ProjetoDecoracaoDto
    {
        public Guid Id { get; set; }

        public Guid PropostaId { get; set; }

        public string Nome { get; set; }

        public string Observacoes { get; set; }

        public string NomeEvento { get; set; }

        public DateTime DataEvento { get; set; }

        public string StatusProposta { get; set; }

        public List<ProjetoArquivoDto> Arquivos { get; set; } = new();
    }
}
