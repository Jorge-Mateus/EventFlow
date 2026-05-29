using EventFlow.Application.DTOs.Contrato;
using EventFlow.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventFlow.Web.Controllers
{
    public class ContratoController : Controller
    {
        private readonly IContratoService _service;

        public ContratoController(
            IContratoService service)
        {
            _service = service;
        }

        public IActionResult Create(Guid eventoId)
        {
            return View(new CriarContratoDto
            {
                EventoId = eventoId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CriarContratoDto dto, IFormFile arquivo)
        {
            dto.TipoArquivo = Path.GetExtension(arquivo.FileName).TrimStart('.').ToLower();

            if (!ModelState.IsValid) return View(dto);

            if (arquivo is not null && arquivo.Length > 0)
            {
                var pastaUploads = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "uploads",
                    "contratos");

                if (!Directory.Exists(pastaUploads))
                    Directory.CreateDirectory(pastaUploads);

                var nomeFisico = $"{Guid.NewGuid()}_{arquivo.FileName}";

                var caminhoFisico = Path.Combine(pastaUploads, nomeFisico);

                using (var stream = new FileStream(caminhoFisico, FileMode.Create))
                {
                    await arquivo.CopyToAsync(stream);
                }

                dto.CaminhoArquivo = $"/uploads/contratos/{nomeFisico}";

                dto.TipoArquivo = string.IsNullOrWhiteSpace(arquivo.ContentType) ? "application/pdf" : arquivo.ContentType;
            }

            await _service.CriarAsync(dto);
            return RedirectToAction("Details", "Evento", new { id = dto.EventoId });

        }
        [HttpPost]
        public async Task<IActionResult> Assinar(
            Guid id,
            Guid eventoId)
        {
            await _service.MarcarAssinadoAsync(id);

            return RedirectToAction(
                "Details",
                "Evento",
                new { id = eventoId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(
            Guid id,
            Guid eventoId)
        {
            await _service.ExcluirAsync(id);

            return RedirectToAction(
                "Details",
                "Evento",
                new { id = eventoId });
        }
    }
}