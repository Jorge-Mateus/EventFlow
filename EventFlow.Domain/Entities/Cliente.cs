namespace EventFlow.Domain.Entities;

public class Cliente : BaseEntity
{
    public string Nome { get;  set; }

    public string Telefone { get;  set; }

    public string Email { get;  set; }

    public ICollection<Proposta> Propostas { get;  set; }
        = new List<Proposta>();

    protected Cliente() { }

    public Cliente(
        string nome,
        string telefone,
        string email)
    {
        Nome = nome;
        Telefone = telefone;
        Email = email;
    }
}