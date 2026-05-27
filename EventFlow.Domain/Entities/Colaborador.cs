using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Domain.Entities
{
    public class Colaborador : BaseEntity
    {
        public string Nome { get; private set; }
        public string CPF { get; set; }
        public string Telefone { get; private set; }

        public string Pix { get; private set; }

        public Guid FuncaoId { get; private set; }

        public Funcao Funcao { get; private set; }

        public bool Ativo { get; private set; }

        protected Colaborador() { }

        public Colaborador(
            string nome, string telefone, string cpf, string pix, Guid funcaoId)
        {
            Nome = nome;
            Telefone = telefone;
            CPF = cpf;
            Pix = pix;
            FuncaoId = funcaoId;
            Ativo = true;
        }

        public void Atualizar(string nome, string telefone, string cpf, string pix, Guid funcaoId)
        {
            Nome = nome;
            Telefone = telefone;
            CPF = cpf;
            Pix = pix;
            FuncaoId = funcaoId;
        }

        public void Desativar()
        {
            Ativo = false;
        }

        public void Ativar()
        {
            Ativo = true;
        }
    }
}
