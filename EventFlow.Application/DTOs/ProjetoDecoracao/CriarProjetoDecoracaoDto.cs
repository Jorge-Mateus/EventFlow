
namespace EventFlow.Application.DTOs.ProjetoDecoracao
{
    public class CriarProjetoDecoracaoDto
    {
        public Guid PropostaId { get; set; }
        public string Nome { get; set; }
        public string Observacoes { get; set; }
        public string? NomeArquivo { get; set; }
        public string? CaminhoArquivo { get; set; }
        public string? TipoArquivo { get; set; }
    }
}
