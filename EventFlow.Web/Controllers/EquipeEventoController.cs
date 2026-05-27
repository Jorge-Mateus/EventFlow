using EventFlow.Application.DTOs.EquipeEvento;
using EventFlow.Application.Interfaces;
using EventFlow.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventFlow.Web.Controllers
{
    public class EquipeEventoController : Controller
    {
        private readonly IEquipeEventoService _service;
        private readonly IEventoService _eventoService;
        private readonly IColaboradorService _colaboradorService;

        public EquipeEventoController(IEquipeEventoService service, IEventoService eventoService, IColaboradorService colaboradorService)
        {
            _service = service;
            _eventoService = eventoService;
            _colaboradorService = colaboradorService;
        }

        public async Task<IActionResult> Index()
        {
            var eventos =
                await _eventoService.ObterTodosAsync();

            return View(eventos);
        }

        public async Task<IActionResult> Create(Guid eventoId)
        {
            await CarregarCombos();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CriarEquipeEventoDto dto)
        {
            if (!ModelState.IsValid)
            {
                await CarregarCombos();
                return View(dto);
            }

            await _service.CriarAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid eventoId)
        {
            var equipe =
                await _service.ObterPorEventoAsync(eventoId);

            await CarregarCombos();

            var dto = new CriarEquipeEventoDto
            {
                EventoId = eventoId,
                Itens = equipe.Select(x =>
                    new CriarEquipeEventoItemDto
                    {
                        ColaboradorId = x.ColaboradorId,
                        ValorPagamento = x.ValorPagamento
                    }).ToList()
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit( CriarEquipeEventoDto dto)
        {
            if (!ModelState.IsValid)
            {
                await CarregarCombos();
                return View(dto);
            }

            await _service.AtualizarAsync(dto);

            return RedirectToAction( "Details","Evento", new { id = dto.EventoId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.RemoverAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task CarregarCombos()
        {
            var eventos = await _eventoService.ObterTodosAsync();

            var colaboradores = await _colaboradorService.ObterTodosAsync();

            ViewBag.Eventos = eventos.Select(x =>
                new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Nome
                });

            ViewBag.Colaboradores = colaboradores.Select(x =>
                new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Nome
                });
        }
    }
}
