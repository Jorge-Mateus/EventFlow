using EventFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventFlow.Web.Controllers
{
    public class DashboardController : Controller
    {

        private readonly IDashboardService _service;

        public DashboardController(IDashboardService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var dashboard =
                await _service.ObterAsync();

            return View(dashboard);
        }
    }
}
