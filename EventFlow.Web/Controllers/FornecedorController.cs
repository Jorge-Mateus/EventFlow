using EventFlow.Application.DTOs.Fornecedor;
using EventFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventFlow.Web.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly IFornecedorService _service;

        public FornecedorController(IFornecedorService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var fornecedores = await _service.ObterTodosAsync();

            return View(fornecedores);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CriarFornecedorDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _service.CriarAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var fornecedor = await _service.ObterPorIdAsync(id);

            if (fornecedor is null) return NotFound();

            var dto = new AtualizarFornecedorDto
            {
                Id = fornecedor.Id,
                Nome = fornecedor.Nome,
                Documento = fornecedor.Documento,
                Telefone = fornecedor.Telefone,
                Email = fornecedor.Email,
                TipoServico = fornecedor.TipoServico
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AtualizarFornecedorDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

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
