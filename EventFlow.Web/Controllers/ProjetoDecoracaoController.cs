using EventFlow.Application.DTOs.ProjetoDecoracao;
using EventFlow.Application.Interfaces;
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