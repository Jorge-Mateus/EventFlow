namespace EventFlow.Domain.Entities;

public class CategoriaOrcamento : BaseEntity
{
    public string Nome { get; private set; }

    public ICollection<PropostaItem> Itens
    { get; private set; }
        = new List<PropostaItem>();

    protected CategoriaOrcamento() { }

    public CategoriaOrcamento(string nome)
    {
        Nome = nome;
    }
}