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
        EmAjuste = 3,
        VisitaTecnicaAgendada = 4,
        EmProjeto3D = 5,
        ProjetoAprovado = 6,
        Aprovada = 7,
        EmMontagem = 8,
        Finalizada = 9,
        Cancelada = 10
    }
}
