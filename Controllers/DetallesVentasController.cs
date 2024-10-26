using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UspgPOS.Data;
using UspgPOS.Models;

namespace UspgPOS.Controllers
{
    public class DetallesVentasController : Controller
    {
        private readonly AppDbContext _context;

        public DetallesVentasController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.DetallesVentas.Include(d => d.Producto).Include(d => d.Venta);
            return View(await appDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallesVenta = await _context.DetallesVentas
                .Include(d => d.Producto)
                .Include(d => d.Venta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detallesVenta == null)
            {
                return NotFound();
            }

            return View(detallesVenta);
        }

        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre");
            ViewData["VentaId"] = new SelectList(_context.Ventas, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VentaId,ProductoId,Cantidad,CantidadTotal")] DetallesVenta detallesVenta)
        {
            _context.Add(detallesVenta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallesVenta = await _context.DetallesVentas.FindAsync(id);
            if (detallesVenta == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", detallesVenta.ProductoId);
            ViewData["VentaId"] = new SelectList(_context.Ventas, "Id", "Id", detallesVenta.VentaId);
            return View(detallesVenta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("Id,VentaId,ProductoId,Cantidad,CantidadTotal")] DetallesVenta detallesVenta)
        {
            if (id != detallesVenta.Id)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(detallesVenta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallesVentaExists(detallesVenta.Id))
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

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallesVenta = await _context.DetallesVentas
                .Include(d => d.Producto)
                .Include(d => d.Venta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detallesVenta == null)
            {
                return NotFound();
            }

            return View(detallesVenta);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var detallesVenta = await _context.DetallesVentas.FindAsync(id);
            if (detallesVenta != null)
            {
                _context.DetallesVentas.Remove(detallesVenta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetallesVentaExists(long? id)
        {
            return _context.DetallesVentas.Any(e => e.Id == id);
        }
    }
}
