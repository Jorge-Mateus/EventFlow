using EventFlow.Application.DTOs.Proposta;
using EventFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventFlow.Web.Controllers
{
    public class PropostaController : Controller
    {
        private readonly IPropostaService _propostaService;
        private readonly IEventoService _eventoService;
        private readonly ICategoriaOrcamentoService _categoriaService;

        public PropostaController(IPropostaService propostaService, IEventoService eventoService, ICategoriaOrcamentoService categoriaService)
        {
            _propostaService = propostaService;
            _eventoService = eventoService;
            _categoriaService = categoriaService;
        }

        public async Task<IActionResult> Index()
        {
            var propostas = await _propostaService.ObterTodosAsync();

            return View(propostas);
        }

        public async Task<IActionResult> Create()
        {
            await CarregarEventos();
            await CarregarCategorias();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CriarPropostaDto dto)
        {
            if (!ModelState.IsValid)
            {
                await CarregarEventos();
                await CarregarCategorias();

                return View(dto);
            }

            await _propostaService.CriarAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var proposta = await _propostaService.ObterDetalheAsync(id);

            if (proposta is null) return NotFound();

            return View(proposta);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var proposta = await _propostaService.ObterPorIdAsync(id);

            if (proposta is null) return NotFound();

            await CarregarEventos();
            await CarregarCategorias();

            var dto = new AtualizarPropostaDto
            {
                Id = proposta.Id,
                EventoId = proposta.EventoId
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AtualizarPropostaDto dto)
        {
            if (!ModelState.IsValid)
            {
                await CarregarEventos();
                await CarregarCategorias();

                return View(dto);
            }

            await _propostaService.AtualizarAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _propostaService.ExcluirAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task CarregarEventos()
        {
            var eventos = await _eventoService.ObterTodosAsync();

            ViewBag.Eventos = eventos.Select(x =>
                    new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Nome
                    });
        }

        private async Task CarregarCategorias()
        {
            var categorias = await _categoriaService.ObterTodosAsync();

            ViewBag.Categorias = categorias.Select(x =>
                    new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Nome
                    });
        }
    }
}