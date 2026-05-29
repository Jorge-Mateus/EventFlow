using EventFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventFlow.Web.Controllers
{
    public class CalendarioController : Controller
    {

        private readonly ICalendarioService _service;

        public CalendarioController(
            ICalendarioService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var eventos =
                await _service.ObterAsync();

            return View(eventos);
        }
    }
}