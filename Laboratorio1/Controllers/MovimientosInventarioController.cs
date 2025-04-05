using System;
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
    public class MovimientosInventarioController : Controller
    {
        private readonly ERPDbContext _context;

        public MovimientosInventarioController(ERPDbContext context)
        {
            _context = context;
        }

        // GET: MovimientosInventario
        public async Task<IActionResult> Index()
        {
            var movimientos = await _context.MovimientoInventarios
                .Where(m => !m.MovimientosInvetarioEliminados)
                .Include(m => m.Producto)
                .Include(m => m.Lote)
                .ToListAsync();

            return View(movimientos);
        }

        // GET: MovimientosInventario/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var movimiento = await _context.MovimientoInventarios
                .Include(m => m.Producto)
                .Include(m => m.Lote)
                .FirstOrDefaultAsync(m => m.MovimientoId == id);

            if (movimiento == null || movimiento.MovimientosInvetarioEliminados)
                return NotFound();

            return View(movimiento);
        }

        // GET: MovimientosInventario/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Productos.Where(p => !p.ProductoEliminado), "ProductoId", "NombreProducto");
            ViewData["LoteId"] = new SelectList(_context.Lotes.Where(l => !l.LoteEliminado), "LoteId", "NumeroLote");
            return View();
        }

        // POST: MovimientosInventario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoMovimiento,Cantidad,EstadoMovimiento,FechaMovimiento,ProductoId,LoteId")] Movimientos_Inventario movimiento)
        {
            var producto = await _context.Productos.FindAsync(movimiento.ProductoId);
            var lote = await _context.Lotes.FindAsync(movimiento.LoteId);

            if (producto != null && !producto.ProductoEliminado && lote != null && !lote.LoteEliminado)
            {
                movimiento.MovimientoId = Guid.NewGuid();
                movimiento.Producto = producto;
                movimiento.Lote = lote;

                _context.Add(movimiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ProductoId"] = new SelectList(_context.Productos.Where(p => !p.ProductoEliminado), "ProductoId", "NombreProducto", movimiento.ProductoId);
            ViewData["LoteId"] = new SelectList(_context.Lotes.Where(l => !l.LoteEliminado), "LoteId", "NumeroLote", movimiento.LoteId);
            return View(movimiento);
        }

        // GET: MovimientosInventario/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var movimiento = await _context.MovimientoInventarios.FindAsync(id);
            if (movimiento == null || movimiento.MovimientosInvetarioEliminados)
                return NotFound();

            ViewData["ProductoId"] = new SelectList(_context.Productos.Where(p => !p.ProductoEliminado), "ProductoId", "NombreProducto", movimiento.ProductoId);
            ViewData["LoteId"] = new SelectList(_context.Lotes.Where(l => !l.LoteEliminado), "LoteId", "NumeroLote", movimiento.LoteId);
            return View(movimiento);
        }

        // POST: MovimientosInventario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MovimientoId,TipoMovimiento,Cantidad,EstadoMovimiento,FechaMovimiento,ProductoId,LoteId")] Movimientos_Inventario movimiento)
        {
            if (id != movimiento.MovimientoId)
                return NotFound();

            var producto = await _context.Productos.FindAsync(movimiento.ProductoId);
            var lote = await _context.Lotes.FindAsync(movimiento.LoteId);

            if (producto != null && !producto.ProductoEliminado && lote != null && !lote.LoteEliminado)
            {
                try
                {
                    movimiento.Producto = producto;
                    movimiento.Lote = lote;

                    _context.Update(movimiento);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimientoExists(movimiento.MovimientoId))
                        return NotFound();

                    throw;
                }
            }

            ViewData["ProductoId"] = new SelectList(_context.Productos.Where(p => !p.ProductoEliminado), "ProductoId", "NombreProducto", movimiento.ProductoId);
            ViewData["LoteId"] = new SelectList(_context.Lotes.Where(l => !l.LoteEliminado), "LoteId", "NumeroLote", movimiento.LoteId);
            return View(movimiento);
        }

        // GET: MovimientosInventario/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var movimiento = await _context.MovimientoInventarios
                .Include(m => m.Producto)
                .Include(m => m.Lote)
                .FirstOrDefaultAsync(m => m.MovimientoId == id);

            if (movimiento == null || movimiento.MovimientosInvetarioEliminados)
                return NotFound();

            return View(movimiento);
        }

        // POST: MovimientosInventario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var movimiento = await _context.MovimientoInventarios.FindAsync(id);
            if (movimiento != null)
            {
                // Eliminación lógica
                movimiento.MovimientosInvetarioEliminados = true;
                _context.Update(movimiento);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool MovimientoExists(Guid id)
        {
            return _context.MovimientoInventarios.Any(m => m.MovimientoId == id);
        }
    }
}
