using EventFlow.Application.DTOs.ProjetoDecoracao;
using EventFlow.Application.Interfaces;
using EventFlow.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventFlow.Web.Controllers
{
    public class ProjetoDecoracaoController : Controller
    {
        private readonly IProjetoDecoracaoService _service;
        private readonly IPropostaService _propostaService;
        private readonly IWebHostEnvironment _environment;
        public ProjetoDecoracaoController(IProjetoDecoracaoService service, IPropostaService propostaService, IWebHostEnvironment environment)
        {
            _service = service;
            _propostaService = propostaService;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var projetos = await _service.ObterTodosAsync();

            return View(projetos);
        }

        public async Task<IActionResult> Create()
        {
            await CarregarPropostas();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CriarProjetoDecoracaoDto dto, IFormFile? arquivo)
        {
            if (!ModelState.IsValid)
            {
                await CarregarPropostas();
                return View(dto);
            }
            var extensoesPermitidas = new[]
            {
                ".png",
                ".jpg",
                ".jpeg",
                ".pdf"
            };

            var extensao = Path.GetExtension(arquivo.FileName).ToLower();

            if (!extensoesPermitidas.Contains(extensao))
            {
                ModelState.AddModelError(
                    "",
                    "Tipo de arquivo não permitido.");

                await CarregarPropostas();

                return View(dto);
            }
            if (arquivo is not null)
            {
                var pasta = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "uploads",
                    "projetos");

                if (!Directory.Exists(pasta))
                    Directory.CreateDirectory(pasta);

                var nomeFisico = $"{Guid.NewGuid()}_{arquivo.FileName}";

                var caminhoCompleto = Path.Combine(pasta, nomeFisico);

                using var stream = new FileStream(caminhoCompleto, FileMode.Create);

                await arquivo.CopyToAsync(stream);

                dto.NomeArquivo = arquivo.FileName;

                dto.CaminhoArquivo = $"/uploads/projetos/{nomeFisico}";

                dto.TipoArquivo = arquivo.ContentType;
            }

            await _service.CriarAsync(dto);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var projeto = await _service.ObterPorIdAsync(id);

            if (projeto is null) return NotFound();

            return View(projeto);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var projeto = await _service.ObterPorIdAsync(id);

            if (projeto is null) return NotFound();

            await CarregarPropostas();

            var dto = new AtualizarProjetoDecoracaoDto
            {
                Id = projeto.Id,
                PropostaId = projeto.PropostaId,
                Nome = projeto.Nome,
                Observacoes = projeto.Observacoes,
                Arquivos = projeto.Arquivos
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AtualizarProjetoDecoracaoDto dto)
        {
            if (!ModelState.IsValid)
            {
                await CarregarPropostas();
                return View(dto);
            }

            await _service.AtualizarAsync(dto);

            return RedirectToAction(nameof(Details), new { id = dto.Id });
        }
        [HttpPost]
        public async Task<IActionResult> UploadArquivos(Guid projetoId, List<IFormFile> arquivos)
        {
            if (arquivos is null || !arquivos.Any())
                return RedirectToAction(nameof(Details), new { id = projetoId });

            var arquivosProcessados = new List<(string Nome, string Caminho, string Tipo)>();

            var pastaUploads = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                "projetos");

            if (!Directory.Exists(pastaUploads))
                Directory.CreateDirectory(pastaUploads);

            foreach (var arquivo in arquivos)
            {
                var nomeFisico = $"{Guid.NewGuid()}_{arquivo.FileName}";

                var caminhoFisico = Path.Combine(pastaUploads, nomeFisico);

                using var stream = new FileStream(caminhoFisico, FileMode.Create);

                await arquivo.CopyToAsync(stream);

                arquivosProcessados.Add((arquivo.FileName, $"/uploads/projetos/{nomeFisico}", arquivo.ContentType));
            }

            await _service.AdicionarArquivosAsync(
                projetoId,
                arquivosProcessados);

            return RedirectToAction(nameof(Details), new { id = projetoId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.ExcluirAsync(id);

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> ExcluirArquivo(Guid arquivoId, Guid projetoId)
        {
            await _service.RemoverArquivoAsync(arquivoId);

            return RedirectToAction(nameof(Edit), new { id = projetoId });
        }

        private async Task CarregarPropostas()
        {
            var propostas = await _propostaService.ObterTodosAsync();

            ViewBag.Propostas = propostas.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Id.ToString()
            });
        }
    }
}