using EventFlow.Application.DTOs.CategoriaOrcamento;
using EventFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventFlow.Web.Controllers
{
    public class CategoriaOrcamentoController
      : Controller
    {
        private readonly
            ICategoriaOrcamentoService
            _service;

        public CategoriaOrcamentoController(
            ICategoriaOrcamentoService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var categorias =
                await _service.ObterTodosAsync();

            return View(categorias);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CriarCategoriaOrcamentoDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _service.CriarAsync(dto);

            return RedirectToAction(nameof(Index));
        }
    }
}
