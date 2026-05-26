using EventFlow.Application.DTOs.VisitaTecnica;
using EventFlow.Application.Interfaces;
using EventFlow.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventFlow.Web.Controllers
{
    public class VisitaTecnicaController : Controller
    {

        private readonly IVisitaTecnicaService _service;
        private readonly IPropostaService _propostaService;
        public VisitaTecnicaController(IVisitaTecnicaService service, IPropostaService propostaService)
        {
            _service = service;
            _propostaService = propostaService;
           
        }

        public async Task<IActionResult> Index()
        {
            var visitas = await _service.ObterTodosAsync();

            return View(visitas);
        }

        public async Task<IActionResult> Create(Guid? propostaId)
        {

            await CarregarPropostas();

            var dto = new CriarVisitaTecnicaDto();

            if (propostaId.HasValue)
                dto.PropostaId = propostaId.Value;

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CriarVisitaTecnicaDto dto)
        {
            if (!ModelState.IsValid)
            {
                await CarregarPropostas();
                return View(dto);
            }

            await _service.CriarAsync(dto);

            return RedirectToAction(
                nameof(Index));
        }

        public async Task<IActionResult> Edit(
            Guid id)
        {
            var visita = await _service.ObterPorIdAsync(id);

            if (visita is null) return NotFound();

            await CarregarPropostas();

            var dto = new AtualizarVisitaTecnicaDto
            {
                Id = visita.Id,
                DataAgendada = visita.DataAgendada,
                Responsavel = visita.Responsavel,
                Observacoes = visita.Observacoes
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AtualizarVisitaTecnicaDto dto)
        {
            if (!ModelState.IsValid)
            {
                await CarregarPropostas();
                return View(dto);
            }

            await _service.AtualizarAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.ExcluirAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task CarregarPropostas()
        {
            var propostas = await _propostaService.ObterTodosAsync();

            ViewBag.Propostas = propostas.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = $"{x.Id} - {x.Status}"
            });
        }
    }
}