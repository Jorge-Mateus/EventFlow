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
    }
}
