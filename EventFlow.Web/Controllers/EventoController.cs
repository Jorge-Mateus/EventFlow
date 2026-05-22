using EventFlow.Application.DTOs.Evento;
using EventFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventFlow.Web.Controllers
{
    public class EventoController : Controller
    {
        private readonly IEventoService _eventoService;

        private readonly IClienteService _clienteService;

        public EventoController(
            IEventoService eventoService,
            IClienteService clienteService)
        {
            _eventoService = eventoService;
            _clienteService = clienteService;
        }

        public async Task<IActionResult> Index()
        {
            var eventos =
                await _eventoService.ObterTodosAsync();

            return View(eventos);
        }

        public async Task<IActionResult> Create()
        {
            await CarregarClientes();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CriarEventoDto dto)
        {
            if (!ModelState.IsValid)
            {
                await CarregarClientes();

                return View(dto);
            }

            await _eventoService.CriarAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        private async Task CarregarClientes()
        {
            var clientes =
                await _clienteService.ObterTodosAsync();

            ViewBag.Clientes =
                clientes.Select(x =>
                    new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Nome
                    });
        }
    }
}
