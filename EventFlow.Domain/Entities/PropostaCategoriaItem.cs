namespace EventFlow.Domain.Entities
{
    public class PropostaCategoriaItem : BaseEntity
    {
        public Guid PropostaCategoriaId { get; private set; }

        public string Descricao { get; private set; }

        public PropostaCategoria PropostaCategoria { get; private set; }

        protected PropostaCategoriaItem() { }

        public PropostaCategoriaItem(string descricao)
        {
            Descricao = descricao;
        }

        public void Atualizar(string descricao)
        {
            Descricao = descricao;
        }
    }
}