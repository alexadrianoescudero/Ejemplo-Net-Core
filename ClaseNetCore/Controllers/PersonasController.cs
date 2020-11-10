using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClaseNetCore.Data;
using ClaseNetCore.Models;
using ClaseNetCore.ViewModel;

namespace ClaseNetCore.Controllers
{
    public class PersonasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Personas
        public async Task<IActionResult> Index()
        {
            List<ViewModelPersonaGenero> lstEnviar = new List<ViewModelPersonaGenero>();
            //var datos1 = await _context.Genero.ToListAsync();
            //foreach (var item in datos1)
            //{
            //var daatospersona = await _context.Persona.ToListAsync();
            //foreach (var item1 in daatospersona)
            //{
            //    ViewModelPersonaGenero Enviar = new ViewModelPersonaGenero();
            //    Enviar.Apellido = item1.Apellido;
            //    Enviar.GeneroPersona = _context.Genero.Where(x=>x.Codigo == 1).FirstOrDefault().Descripcion;
            //    lstEnviar.Add(Enviar);
            //}

            //}


            var datos = _context.Persona.Select(q => new ViewModelPersonaGenero
            {
                Apellido = q.Apellido,
                Codigo = q.Codigo,
                Nombre = q.Nombre, 
                DocumentoPersona = q.CodigoDocumentoNavigation.Descripcion,
                GeneroPersona = q.CodigoGeneroNavigation.Descripcion
            }).ToListAsync();
            return View(await datos);
            //return View(lstEnviar);
            //var applicationDbContext = _context.Persona.Include(p => p.CodigoGeneroNavigation);
            //return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> MasculinoIndex()
        {


            var datos = _context.Persona.Select(q => new ViewModelPersonaGenero
            {
                Apellido = q.Apellido,
                Codigo = q.Codigo,
                Nombre = q.Nombre,
                Estado = q.Estado,
                GeneroPersona = q.CodigoGeneroNavigation.Descripcion
            }).Where(x => x.Estado == 1 && x.GeneroPersona == "Masculino").ToListAsync();
            return View(await datos);

            //var applicationDbContext = _context.Persona.Include(p => p.CodigoGeneroNavigation);
            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: Personas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Persona
                .Include(p => p.CodigoGeneroNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // GET: Personas/Create
        public IActionResult Create()
        {
            ViewData["CodigoGenero"] = new SelectList(_context.Genero, "Codigo", "Descripcion");
            ViewData["CodigoDocumento"] = new SelectList(_context.Documento, "Codigo", "Descripcion");
            return View();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nombre,Apellido,Estado,CodigoGenero,CodigoDocumento")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                _context.Add(persona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoGenero"] = new SelectList(_context.Genero, "Codigo", "Descripcion", persona.CodigoGenero);
            ViewData["CodigoDocumento"] = new SelectList(_context.Documento, "Codigo", "Descripcion", persona.CodigoDocumento);
            return View(persona);
        }

        // GET: Personas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Persona.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            ViewData["CodigoGenero"] = new SelectList(_context.Genero, "Codigo", "Descripcion", persona.CodigoGenero);
            ViewData["CodigoDocumento"] = new SelectList(_context.Documento, "Codigo", "Descripcion", persona.CodigoDocumento);
            return View(persona);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Nombre,Apellido,Estado,CodigoGenero,CodigoDocumento")] Persona persona)
        {
            if (id != persona.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(persona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(persona.Codigo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoGenero"] = new SelectList(_context.Genero, "Codigo", "Descripcion", persona.CodigoGenero);
            ViewData["CodigoDocumento"] = new SelectList(_context.Documento, "Codigo", "Descripcion", persona.CodigoDocumento);
            return View(persona);
        }

        // GET: Personas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Persona
                .Include(p => p.CodigoGeneroNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var persona = await _context.Persona.FindAsync(id);
            _context.Persona.Remove(persona);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaExists(int id)
        {
            return _context.Persona.Any(e => e.Codigo == id);
        }
    }
}
