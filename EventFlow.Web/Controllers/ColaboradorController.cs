using EventFlow.Application.DTOs.Colaborador;
using EventFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventFlow.Web.Controllers
{
    public class ColaboradorController : Controller
    {
        private readonly IColaboradorService _service;
        private readonly IFuncaoService _funcaoService;

        public ColaboradorController(IColaboradorService service, IFuncaoService funcaoService)
        {
            _service = service;
            _funcaoService = funcaoService;
        }

        public async Task<IActionResult> Index()
        {
            var colaboradores = await _service.ObterTodosAsync();

            return View(colaboradores);
        }

        public async Task<IActionResult> Create()
        {
            await CarregarFuncoes();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CriarColaboradorDto dto)
        {
            if (!ModelState.IsValid)
            {
                await CarregarFuncoes();
                return View(dto);
            }

            await _service.CriarAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var colaborador = await _service.ObterPorIdAsync(id);

            if (colaborador is null)
                return NotFound();

            await CarregarFuncoes();

            var dto = new AtualizarColaboradorDto
            {
                Id = colaborador.Id,
                Nome = colaborador.Nome,
                Telefone = colaborador.Telefone,
                CPF = colaborador.CPF,
                Pix = colaborador.Pix,
                FuncaoId = colaborador.FuncaoId
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AtualizarColaboradorDto dto)
        {
            if (!ModelState.IsValid)
            {
                await CarregarFuncoes();
                return View(dto);
            }

            await _service.AtualizarAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.RemoverAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task CarregarFuncoes()
        {
            var funcoes = await _funcaoService.ObterTodosAsync();

            ViewBag.Funcoes = funcoes.Select(x =>
                new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Nome
                });
        }
    }
}
