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

    public class ProductosController : Controller
    {
        private readonly ERPDbContext _context;

        public ProductosController(ERPDbContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            var productosActivos = await _context.Productos
                .Where(p => !p.ProductoEliminado)
                .Include(p => p.CategoriaProducto)
                .Include(p => p.Proveedor)
                .ToListAsync();

            return View(productosActivos);
        }


        // GET: Productos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.CategoriaProducto)
                .Include(p => p.Proveedor)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaProductoId"] = new SelectList(_context.CategoriaProductos.Where(c => !c.CategoriaProductoEliminada), "CategoriaProductoId", "NombreCategoriaProducto");
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores.Where(p => !p.ProveedorEliminado), "ProveedorId", "NombreProveedor");
            return View();
        }

        // POST: Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductoId,NombreProducto,DescripcionProducto,UnidadMedidaProducto,CategoriaProductoId,ProveedorId,PrecioCompra,PrecioVenta,StockMinimoProducto,StockMaximoProducto,EstadoProducto")] Producto producto)
        {
            var categoriaProducto = await _context.CategoriaProductos.FindAsync(producto.CategoriaProductoId);
            var proveedor = await _context.Proveedores.FindAsync(producto.ProveedorId);

            if (categoriaProducto != null && !categoriaProducto.CategoriaProductoEliminada && proveedor != null && !proveedor.ProveedorEliminado)
            {
                producto.CategoriaProducto = categoriaProducto;
                producto.Proveedor = proveedor;
                producto.ProductoId = Guid.NewGuid();
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoriaProductoId"] = new SelectList(_context.CategoriaProductos.Where(c => !c.CategoriaProductoEliminada), "CategoriaProductoId", "NombreCategoriaProducto", producto.CategoriaProductoId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores.Where(p => !p.ProveedorEliminado), "ProveedorId", "NombreProveedor", producto.ProveedorId);
            return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaProductoId"] = new SelectList(_context.CategoriaProductos.Where(c => !c.CategoriaProductoEliminada), "CategoriaProductoId", "NombreCategoriaProducto", producto.CategoriaProductoId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores.Where(p => !p.ProveedorEliminado), "ProveedorId", "NombreProveedor", producto.ProveedorId);
            return View(producto);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProductoId,NombreProducto,DescripcionProducto,UnidadMedidaProducto,CategoriaProductoId,ProveedorId,PrecioCompra,PrecioVenta,StockMinimoProducto,StockMaximoProducto,EstadoProducto")] Producto producto)
        {
            if (id != producto.ProductoId)
            {
                return NotFound();
            }

            var categoriaProducto = await _context.CategoriaProductos.FindAsync(producto.CategoriaProductoId);
            var proveedor = await _context.Proveedores.FindAsync(producto.ProveedorId);

            if (categoriaProducto != null && !categoriaProducto.CategoriaProductoEliminada && proveedor != null && !proveedor.ProveedorEliminado)
            {
                producto.CategoriaProducto = categoriaProducto;
                producto.Proveedor = proveedor;

                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.ProductoId))
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

            ViewData["CategoriaProductoId"] = new SelectList(_context.CategoriaProductos.Where(c => !c.CategoriaProductoEliminada), "CategoriaProductoId", "NombreCategoriaProducto", producto.CategoriaProductoId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores.Where(p => !p.ProveedorEliminado), "ProveedorId", "NombreProveedor", producto.ProveedorId);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.CategoriaProducto)
                .Include(p => p.Proveedor)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                producto.ProductoEliminado = true;
                _context.Productos.Update(producto);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(Guid id)
        {
            return _context.Productos.Any(e => e.ProductoId == id);
        }
    }
}
