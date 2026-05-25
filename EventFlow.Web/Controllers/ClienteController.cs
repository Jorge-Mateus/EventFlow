using EventFlow.Application.DTOs.Cliente;
using EventFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventFlow.Web.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _service;

        public ClienteController(IClienteService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var clientes = await _service.ObterTodosAsync();

            return View(clientes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CriarClienteDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _service.CriarAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var cliente = await _service.ObterDetalheAsync(id);

            if (cliente is null) return NotFound();

            return View(cliente);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var cliente = await _service.ObterDetalheAsync(id);

            if (cliente is null) return NotFound();

            var dto = new AtualizarClienteDto
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                Email = cliente.Email
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AtualizarClienteDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

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
