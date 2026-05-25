using EventFlow.Application.DTOs.Evento;
using EventFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventFlow.Web.Controllers;

public class EventoController : Controller
{
    private readonly IEventoService _eventoService;
    private readonly IClienteService _clienteService;

    public EventoController(IEventoService eventoService, IClienteService clienteService)
    {
        _eventoService = eventoService;
        _clienteService = clienteService;
    }

    public async Task<IActionResult> Index()
    {
        var eventos = await _eventoService.ObterTodosAsync();

        return View(eventos);
    }

    public async Task<IActionResult> Create()
    {
        await CarregarClientes();

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CriarEventoDto dto)
    {
        if (!ModelState.IsValid)
        {
            await CarregarClientes();
            return View(dto);
        }

        await _eventoService.CriarAsync(dto);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var evento =
            await _eventoService.ObterDetalheAsync(id);

        if (evento is null) return NotFound();

        return View(evento);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var evento = await _eventoService.ObterDetalheAsync(id);

        if (evento is null) return NotFound();

        await CarregarClientes();

        var dto = new AtualizarEventoDto
        {
            Id = evento.Id,
            ClienteId = evento.ClienteId,
            Nome = evento.Nome,
            DataEvento = evento.DataEvento,
            LocalEvento = evento.LocalEvento,
            QuantidadeConvidados = evento.QuantidadeConvidados
        };

        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(AtualizarEventoDto dto)
    {
        if (!ModelState.IsValid)
        {
            await CarregarClientes();
            return View(dto);
        }

        await _eventoService.AtualizarAsync(dto);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _eventoService.ExcluirAsync(id);

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