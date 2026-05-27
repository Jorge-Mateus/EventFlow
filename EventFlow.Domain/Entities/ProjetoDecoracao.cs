using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Domain.Entities
{
    public class ProjetoDecoracao : BaseEntity
    {
        public Guid PropostaId { get; private set; }

        public Proposta Proposta { get; private set; }

        public string Nome { get; private set; }

        public string Observacoes { get; private set; }

        private readonly List<ProjetoArquivo> _arquivos = new();

        public IReadOnlyCollection<ProjetoArquivo> Arquivos => _arquivos.AsReadOnly();

        protected ProjetoDecoracao() { }

        public ProjetoDecoracao(Guid propostaId, string nome, string observacoes)
        {
            PropostaId = propostaId;
            Nome = nome;
            Observacoes = observacoes;
        }

        public void AdicionarArquivo(ProjetoArquivo arquivo)
        {
            _arquivos.Add(arquivo);
        }
    }
}
