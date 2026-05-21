using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Domain.Enums
{
    public enum StatusProposta
    {
        Rascunho = 1,
        Enviada = 2,
        EmNegociacao = 3,
        Aprovada = 4,
        Rejeitada = 5,
        Cancelada = 6
    }
}
