using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Domain.Entities;

public class PropostaItem : BaseEntity
{
    public Guid PropostaId { get; private set; }

    public Proposta Proposta { get; private set; }

    public string Descricao { get; private set; }

    public int Quantidade { get; private set; }

    public decimal ValorUnitario { get; private set; }

    public decimal Total =>
        Quantidade * ValorUnitario;

    protected PropostaItem() { }

    public PropostaItem(
        string descricao,
        int quantidade,
        decimal valorUnitario)
    {
        Descricao = descricao;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
    }
}