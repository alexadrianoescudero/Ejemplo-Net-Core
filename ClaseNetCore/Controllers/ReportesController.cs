using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClaseNetCore.Data;
using ClaseNetCore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ClaseNetCore.Controllers
{
    public class ReportesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportesController(ApplicationDbContext context)
        {
            _context = context;
        }
       
        // GET: Categorias
        public async Task<IActionResult> Index()
        {
            ViewData["CorreoLocal"] = new SelectList(_context.Genero.Where(x => x.Estado == 1), "Codigo", "Descripcion");
            return View();
        }
        [HttpGet]
        [Route("reporteProductos")]
        public async Task<JsonResult> reporteProductos(string correo)
        {
            List<ViewModelPersonaGenero> datos1 = new List<ViewModelPersonaGenero>();
            string datosenvia = string.Empty;
            try
            {
                var datos =  _context.Persona.Select(x => new ViewModelPersonaGenero
                {
                    Nombre = string.Format("{0} {1}", x.Nombre, x.Apellido),
                   GeneroPersona = x.CodigoGeneroNavigation.Descripcion,
                   CodigoGenero = x.CodigoGenero
                }).Where(x => x.CodigoGenero == Convert.ToInt32(correo)).ToList();

                var datos12 = datos.GroupBy(u => u.Nombre)
                                      .Select(grp => new { label = grp.Key, y = grp.Sum(x => x.Codigo) })
                                      .ToList();
                datosenvia = JsonConvert.SerializeObject(datos12);
            }
            catch
            {

            }
            return Json(datosenvia);
        }
    }
}