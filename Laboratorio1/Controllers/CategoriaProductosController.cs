

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laboratorio1.Models;
using Microsoft.AspNetCore.Authorization;

namespace Laboratorio1.Controllers
{
    [Authorize]
    public class CategoriaProductosController : Controller
    {
        private readonly ERPDbContext _context;

        public CategoriaProductosController(ERPDbContext context)
        {
            _context = context;
        }

        // GET: CategoriaProductos
        // Filtro para mostrar solo Categorias de Productos que no estan eliminadas

        public async Task<IActionResult> Index()
        {
            var categoriasActivas = await _context.CategoriaProductos
                .Where(c => !c.CategoriaProductoEliminada)
                .ToListAsync();

            return View(categoriasActivas);
        }




        // GET: CategoriaProductos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaProducto = await _context.CategoriaProductos
                .FirstOrDefaultAsync(m => m.CategoriaProductoId == id);
            if (categoriaProducto == null)
            {
                return NotFound();
            }

            return View(categoriaProducto);
        }

        // GET: CategoriaProductos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriaProductos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoriaProductoId,NombreCategoriaProducto,DescripcionCategoriaProducto,EstadoCategoriaProducto")] CategoriaProducto categoriaProducto)
        {
            if (ModelState.IsValid)
            {
                categoriaProducto.CategoriaProductoId = Guid.NewGuid();
                _context.Add(categoriaProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaProducto);
        }

        // GET: CategoriaProductos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaProducto = await _context.CategoriaProductos.FindAsync(id);
            if (categoriaProducto == null)
            {
                return NotFound();
            }
            return View(categoriaProducto);
        }

        // POST: CategoriaProductos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CategoriaProductoId,NombreCategoriaProducto,DescripcionCategoriaProducto,EstadoCategoriaProducto")] CategoriaProducto categoriaProducto)
        {
            if (id != categoriaProducto.CategoriaProductoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriaProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaProductoExists(categoriaProducto.CategoriaProductoId))
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
            return View(categoriaProducto);
        }

        // GET: CategoriaProductos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaProducto = await _context.CategoriaProductos
                .FirstOrDefaultAsync(m => m.CategoriaProductoId == id);
            if (categoriaProducto == null)
            {
                return NotFound();
            }

            return View(categoriaProducto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var categoriaProducto = await _context.CategoriaProductos.FindAsync(id);
            if (categoriaProducto != null)
            {
                categoriaProducto.CategoriaProductoEliminada = true;
                _context.CategoriaProductos.Update(categoriaProducto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaProductoExists(Guid id)
        {
            return _context.CategoriaProductos.Any(e => e.CategoriaProductoId == id);
        }
    }
}
