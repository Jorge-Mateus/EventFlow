using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Domain.Entities
{
    public class Funcao : BaseEntity
    {
        public string Nome { get; private set; }
        protected Funcao() { }
        public Funcao(string nome)
        {
            Nome = nome;
        }

        public void Atualizar(string nome)
        {
            Nome = nome;
        }
    }
}
