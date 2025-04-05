

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
    public class ProveedoresController : Controller
    {
        private readonly ERPDbContext _context;

        public ProveedoresController(ERPDbContext context)
        {
            _context = context;
        }

        // GET: Proveedores - solo mostrar registros que tiene 0 en la columna de eliminacion
        public async Task<IActionResult> Index()
        {
            var proveedoresActivos = await _context.Proveedores
                .Where(p => !p.ProveedorEliminado)
                .ToListAsync();

            return View(proveedoresActivos);
        }


        // GET: Proveedores/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.ProveedorId == id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }

        // GET: Proveedores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proveedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProveedorId,NombreProveedor,NitProveedor,DireccionProveedor,TelefonoProveedor,EstadoProveedor")] Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                proveedor.ProveedorId = Guid.NewGuid();
                _context.Add(proveedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proveedor);
        }

        // GET: Proveedores/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }
            return View(proveedor);
        }

        // POST: Proveedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProveedorId,NombreProveedor,NitProveedor,DireccionProveedor,TelefonoProveedor,EstadoProveedor")] Proveedor proveedor)
        {
            if (id != proveedor.ProveedorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proveedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedorExists(proveedor.ProveedorId))
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
            return View(proveedor);
        }

        // GET: Proveedores/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.ProveedorId == id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }

        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor != null)
            {
                proveedor.ProveedorEliminado = true;
                _context.Proveedores.Update(proveedor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProveedorExists(Guid id)
        {
            return _context.Proveedores.Any(e => e.ProveedorId == id);
        }
    }
}
