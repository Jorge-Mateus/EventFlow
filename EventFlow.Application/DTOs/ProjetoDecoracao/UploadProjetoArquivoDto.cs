using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs.ProjetoDecoracao
{
    public class UploadProjetoArquivoDto
    {
        public Guid ProjetoDecoracaoId { get; set; }
        public string NomeArquivo { get; set; }
        public string Caminho { get; set; }
        public string Tipo { get; set; }
    }
}
