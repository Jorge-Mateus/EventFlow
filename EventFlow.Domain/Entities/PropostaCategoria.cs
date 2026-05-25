using System;
using System.Collections.ObjectModel;

namespace EventFlow.Domain.Entities
{
    public class PropostaCategoria : BaseEntity
    {
        private readonly List<PropostaCategoriaItem>_itens = new();

        public Guid PropostaId { get; private set; }

        public Guid CategoriaOrcamentoId { get; private set; }

        public decimal Valor { get; private set; }

        public Proposta Proposta { get; private set; }

        public CategoriaOrcamento CategoriaOrcamento { get; private set; }

        public IReadOnlyCollection<PropostaCategoriaItem> Itens => _itens.AsReadOnly();

        protected PropostaCategoria() { }

        public PropostaCategoria(Guid categoriaOrcamentoId, decimal valor)
        {
            CategoriaOrcamentoId = categoriaOrcamentoId;
            Valor = valor;
        }

        public void CarregarItens(IEnumerable<PropostaCategoriaItem> itens)
        {
            _itens.Clear();
            _itens.AddRange(itens);
        }

        public void AdicionarItem(string descricao)
        {
            _itens.Add(new PropostaCategoriaItem(descricao));
        }
    }
}