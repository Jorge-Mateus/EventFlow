using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Domain.Entities
{
    public class Fornecedor : BaseEntity
    {
        public string Nome { get; private set; }

        public string Documento { get; private set; }

        public string Telefone { get; private set; }

        public string Email { get; private set; }

        public string TipoServico { get; private set; }

        public ICollection<EventoFornecedor> Eventos { get; private set; } = new List<EventoFornecedor>();

        protected Fornecedor() { }

        public Fornecedor(string nome, string documento, string telefone, string email, string tipoServico)
        {
            Nome = nome;
            Documento = documento;
            Telefone = telefone;
            Email = email;
            TipoServico = tipoServico;
        }

        public void Atualizar(string nome, string documento, string telefone, string email, string tipoServico)
        {
            Nome = nome;
            Documento = documento;
            Telefone = telefone;
            Email = email;
            TipoServico = tipoServico;
        }
    }
}
