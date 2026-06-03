using Microsoft.AspNetCore.Mvc;
using MvcZapatillasExamen.Models;
using MvcZapatillasExamen.Services;


namespace MvcZapatillasExamen.Controllers
{
    public class ZapatillasController : Controller
    {
        private ZapatillasService _service;

        public ZapatillasController(ZapatillasService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var zapatillas = await _service.GetZapatillasAsync();
            return View(zapatillas);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Zapatilla zapatilla, IFormFile imagenZapatilla)
        {
            await _service.CrearZapatillaAsync(zapatilla, imagenZapatilla);
            return RedirectToAction("Index");
        }
    }
}