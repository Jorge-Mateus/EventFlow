using EventFlow.Application.DTOs.CategoriaOrcamento;
using EventFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventFlow.Web.Controllers
{
    public class CategoriaOrcamentoController
      : Controller
    {
        private readonly ICategoriaOrcamentoService _service;

        public CategoriaOrcamentoController(ICategoriaOrcamentoService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var categorias = await _service.ObterTodosAsync();

            return View(categorias);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CriarCategoriaOrcamentoDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _service.CriarAsync(dto);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var categoria = await _service.ObterDetalheAsync(id);

            if (categoria is null) return NotFound();

            return View(categoria);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var categoria = await _service.ObterDetalheAsync(id);

            if (categoria is null) return NotFound();

            var dto = new AtualizarCategoriaOrcamentoDto
            {
                Id = categoria.Id,
                Nome = categoria.Nome
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AtualizarCategoriaOrcamentoDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _service.AtualizarAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.ExcluirAsync(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
