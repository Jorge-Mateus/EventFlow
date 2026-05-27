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
    }
}
