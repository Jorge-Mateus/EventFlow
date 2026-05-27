using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.DTOs.Colaborador
{
    public class CriarColaboradorDto
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }
        public string Pix { get; set; }
        public Guid FuncaoId { get; set; }
    }
}
