using EventFlow.Application.DTOs.MovimentacaoFinanceira;
using EventFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventFlow.Web.Controllers
{
    public class MovimentacaoFinanceiraController : Controller
    {
        private readonly IMovimentacaoFinanceiraService _service;

        public MovimentacaoFinanceiraController(IMovimentacaoFinanceiraService service)
        {
            _service = service;
        }

        public IActionResult Create(Guid eventoId)
        {
            return View(new CriarMovimentacaoFinanceiraDto
            {
                EventoId = eventoId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CriarMovimentacaoFinanceiraDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _service.CriarAsync(dto);

            return RedirectToAction("Details", "Evento", new { id = dto.EventoId });
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var movimentacoes = await _service.ObterPorEventoAsync(Guid.Empty);

            var movimentacao = movimentacoes.FirstOrDefault(x => x.Id == id);

            if (movimentacao is null) return NotFound();

            var dto = new AtualizarMovimentacaoFinanceiraDto
            {
                Id = movimentacao.Id,
                Descricao = movimentacao.Descricao,
                Valor = movimentacao.Valor,
                Tipo = movimentacao.Tipo
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AtualizarMovimentacaoFinanceiraDto dto)
        {
            await _service.AtualizarAsync(dto);

            return RedirectToAction("Index", "Evento");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.ExcluirAsync(id);

            return RedirectToAction("Index", "Evento");
        }
    }
}
