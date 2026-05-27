using EventFlow.Application.DTOs.ProjetoDecoracao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Interfaces
{
    public interface IProjetoDecoracaoService
    {
        Task CriarAsync(CriarProjetoDecoracaoDto dto);
        Task<IEnumerable<ProjetoDecoracaoDto>> ObterTodosAsync();
        Task<ProjetoDecoracaoDto?> ObterPorIdAsync(Guid id);
        Task UploadArquivoAsync(UploadProjetoArquivoDto dto);
        Task<ProjetoDecoracaoDto?> ObterDetalheAsync(Guid id);
        Task AtualizarAsync(AtualizarProjetoDecoracaoDto dto);
        Task ExcluirAsync(Guid id);
        Task AdicionarArquivosAsync(Guid projetoId, List<(string Nome, string Caminho, string Tipo)> arquivos);
        Task AtualizarComArquivosAsync(AtualizarProjetoDecoracaoDto dto, List<(string Nome, string Caminho, string Tipo)> arquivos);
        Task RemoverArquivoAsync(Guid arquivoId);
    }
}
