using EventFlow.Application.DTOs.Fornecedor;
using EventFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventFlow.Web.Controllers
{
    public class EventoFornecedorController : Controller
    {

        private readonly IEventoFornecedorService _service;
        private readonly IFornecedorService _fornecedorService;

        public EventoFornecedorController(IEventoFornecedorService service, IFornecedorService fornecedorService)
        {
            _service = service;
            _fornecedorService = fornecedorService;
        }

        public async Task<IActionResult> Create(Guid eventoId)
        {
            await CarregarFornecedores();

            return View(new CriarEventoFornecedorDto
            {
                EventoId = eventoId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CriarEventoFornecedorDto dto)
        {
            await _service.CriarAsync(dto);

            return RedirectToAction("Details", "Evento", new { id = dto.EventoId });
        }

        public async Task<IActionResult> Edit(Guid eventoId)
        {
            var itens = await _service.ObterPorEventoAsync(eventoId);

            await CarregarFornecedores();

            return View(new CriarEventoFornecedorDto
            {
                EventoId = eventoId,
                Itens = itens.ToList()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CriarEventoFornecedorDto dto)
        {
            await _service.AtualizarAsync(dto);

            return RedirectToAction("Details", "Evento", new { id = dto.EventoId });
        }

        private async Task CarregarFornecedores()
        {
            var fornecedores = await _fornecedorService.ObterTodosAsync();

            ViewBag.Fornecedores = fornecedores.Select(x =>
                    new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = $"{x.Nome} - {x.TipoServico}"
                    });
        }
    }
}
