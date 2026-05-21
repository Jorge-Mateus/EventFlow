using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Domain.Entities
{
    public class Cliente : BaseEntity
    {
        public string Nome { get; private set; }

        public string Telefone { get; private set; }

        public string Email { get; private set; }

        protected Cliente() { }

        public Cliente(string nome, string telefone, string email)
        {
            Nome = nome;
            Telefone = telefone;
            Email = email;
        }

    }
}
