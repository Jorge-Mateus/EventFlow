using EventFlow.Application.DTOs.Funcao;
using EventFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventFlow.Web.Controllers
{
    public class FuncaoController : Controller
    {
        private readonly IFuncaoService _service;

        public FuncaoController(IFuncaoService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var funcoes = await _service.ObterTodosAsync();

            return View(funcoes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CriarFuncaoDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _service.CriarAsync(dto);

            return RedirectToAction(
                nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var funcao = await _service.ObterPorIdAsync(id);

            if (funcao is null)
                return NotFound();

            var dto =
                new AtualizarFuncaoDto
                {
                    Id = funcao.Id,
                    Nome = funcao.Nome
                };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AtualizarFuncaoDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _service.AtualizarAsync(dto);

            return RedirectToAction(
                nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.RemoverAsync(id);

            return RedirectToAction(
                nameof(Index));
        }
    }
}
