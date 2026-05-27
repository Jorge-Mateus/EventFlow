using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Domain.Entities
{
    public class ProjetoArquivo : BaseEntity
    {
        public Guid ProjetoDecoracaoId { get; private set; }

        public ProjetoDecoracao ProjetoDecoracao { get; private set; }

        public string NomeArquivo { get; private set; }

        public string Caminho { get; private set; }

        public string Tipo { get; private set; }

        protected ProjetoArquivo() { }

        public ProjetoArquivo(string nomeArquivo, string caminho, string tipo)
        {
            NomeArquivo = nomeArquivo;
            Caminho = caminho;
            Tipo = tipo;
        }
    }
}
