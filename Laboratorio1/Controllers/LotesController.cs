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
	public class LotesController : Controller
	{
		private readonly ERPDbContext _context;

		public LotesController(ERPDbContext context)
		{
			_context = context;
		}

		// GET: Lotes
		public async Task<IActionResult> Index()
		{
			var eRPDbContext = _context.Lotes
				.Include(l => l.Producto)
				.Where(l => !l.LoteEliminado); // Solo lotes no eliminados

			return View(await eRPDbContext.ToListAsync());
		}

		// GET: Lotes/Details/5
		public async Task<IActionResult> Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var lote = await _context.Lotes
				.Include(l => l.Producto)
				.FirstOrDefaultAsync(m => m.LoteId == id);
			if (lote == null)
			{
				return NotFound();
			}

			return View(lote);
		}

		// GET: Lotes/Create
		public IActionResult Create()
		{
			ViewData["ProductoId"] = new SelectList(
				_context.Productos.Where(p => !p.ProductoEliminado),
				"ProductoId",
				"NombreProducto"
			);
			return View();
		}

		// POST: Lotes/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ProductoId,NumeroLote,FechaVencimientoLote,EstadoLote")] Lote lote)
		{
			var producto = await _context.Productos.FindAsync(lote.ProductoId);

			if (producto != null && !producto.ProductoEliminado)
			{
				lote.Producto = producto;
				lote.LoteId = Guid.NewGuid();
				lote.LoteEliminado = false;

				_context.Add(lote);
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}

			// En caso de error, volver a cargar ViewData con productos válidos
			ViewData["ProductoId"] = new SelectList(
				_context.Productos.Where(p => !p.ProductoEliminado),
				"ProductoId",
				"NombreProducto",
				lote.ProductoId
			);

			return View(lote);
		}



		// GET: Lotes/Edit/5
		public async Task<IActionResult> Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var lote = await _context.Lotes.FindAsync(id);
			if (lote == null)
			{
				return NotFound();
			}

			ViewData["ProductoId"] = new SelectList(
				_context.Productos.Where(p => !p.ProductoEliminado),
				"ProductoId",
				"NombreProducto",
				lote.ProductoId
			);

			return View(lote);
		}

		// POST: Lotes/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("LoteId,ProductoId,NumeroLote,FechaVencimientoLote,EstadoLote")] Lote lote)
		{
			if (id != lote.LoteId)
			{
				return NotFound();
			}

			var producto = await _context.Productos.FindAsync(lote.ProductoId);

			if (producto != null && !producto.ProductoEliminado)
			{
				lote.Producto = producto;

				try
				{
					_context.Update(lote);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!_context.Lotes.Any(e => e.LoteId == lote.LoteId))
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

			ViewData["ProductoId"] = new SelectList(
				_context.Productos.Where(p => !p.ProductoEliminado),
				"ProductoId",
				"NombreProducto",
				lote.ProductoId
			);

			return View(lote);
		}


		// GET: Lotes/Delete/5
		public async Task<IActionResult> Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var lote = await _context.Lotes
				.Include(l => l.Producto)
				.FirstOrDefaultAsync(m => m.LoteId == id);
			if (lote == null)
			{
				return NotFound();
			}

			return View(lote);
		}

		// POST: Lotes/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var lote = await _context.Lotes.FindAsync(id);
			if (lote != null)
			{
				lote.LoteEliminado = true;
				_context.Lotes.Update(lote);
			}
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}


		private bool LoteExists(Guid id)
		{
			return _context.Lotes.Any(e => e.LoteId == id);
		}
	}
}