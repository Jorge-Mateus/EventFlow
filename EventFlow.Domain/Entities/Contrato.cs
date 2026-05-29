using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Domain.Entities
{
    public class Contrato : BaseEntity
    {
        public Guid EventoId { get; private set; }
        public Evento Evento { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string CaminhoArquivo { get; private set; }
        public string TipoArquivo { get; private set; }
        public bool Assinado { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataAssinatura { get; private set; }

        protected Contrato() { }

        public Contrato(Guid eventoId, string titulo, string descricao, string caminhoArquivo, string tipoArquivo)
        {
            EventoId = eventoId;
            Titulo = titulo;
            Descricao = descricao;
            CaminhoArquivo = caminhoArquivo;
            TipoArquivo = tipoArquivo;
            DataCriacao = DateTime.Now;
            Assinado = false;
        }

        public void MarcarComoAssinado()
        {
            Assinado = true;
            DataAssinatura = DateTime.Now;
        }

        public void Atualizar(string titulo, string descricao, string caminhoArquivo, string tipoArquivo)
        {
            Titulo = titulo;
            Descricao = descricao;
            CaminhoArquivo = caminhoArquivo;
            TipoArquivo = tipoArquivo;
        }
    }
}
