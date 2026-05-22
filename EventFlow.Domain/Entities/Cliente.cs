namespace EventFlow.Domain.Entities;

public class Cliente : BaseEntity
{
    public string Nome { get; private set; }

    public string Telefone { get; private set; }

    public string Email { get; private set; }

    public ICollection<Evento> Eventos { get; private set; } = new List<Evento>();

    protected Cliente() { }

    public Cliente(string nome, string telefone, string email)
    {
        Nome = nome;
        Telefone = telefone;
        Email = email;
    }
}